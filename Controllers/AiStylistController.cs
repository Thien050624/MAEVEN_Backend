using System.Net.Http.Json;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using MAEVEN.Backend.Data;
using MAEVEN.Backend.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MAEVEN.Backend.Controllers;

[ApiController]
[Authorize]
[Route("api/ai-stylist")]
public class AiStylistController : ControllerBase
{
    private static readonly JsonSerializerOptions JsonOptions = new(JsonSerializerDefaults.Web);
    private readonly AppDbContext _dbContext;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IConfiguration _configuration;
    private readonly ILogger<AiStylistController> _logger;

    public AiStylistController(
        AppDbContext dbContext,
        IHttpClientFactory httpClientFactory,
        IConfiguration configuration,
        ILogger<AiStylistController> logger)
    {
        _dbContext = dbContext;
        _httpClientFactory = httpClientFactory;
        _configuration = configuration;
        _logger = logger;
    }

    [HttpPost]
    public async Task<ActionResult<AiStylistResponseDto>> CreateRecommendation(AiStylistRequestDto request, CancellationToken cancellationToken)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrWhiteSpace(userId))
        {
            return Unauthorized();
        }

        if (string.IsNullOrWhiteSpace(request.Message))
        {
            return BadRequest(new { message = "Please describe the outfit or occasion you need help with." });
        }

        var products = await _dbContext.Products
            .AsNoTracking()
            .Include(product => product.Images)
            .Include(product => product.Colors)
            .Include(product => product.Sizes)
            .Include(product => product.Tags)
            .Include(product => product.Specifications)
            .Include(product => product.Reviews)
            .Where(product => product.InStock)
            .ToListAsync(cancellationToken);

        if (products.Count == 0)
        {
            return StatusCode(StatusCodes.Status503ServiceUnavailable, new { message = "No products are available for styling right now." });
        }

        var apiKey = _configuration["Gemini:ApiKey"] ?? _configuration["GEMINI_API_KEY"];
        if (string.IsNullOrWhiteSpace(apiKey))
        {
            _logger.LogWarning("Gemini API key is not configured.");
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = "AI Stylist is not configured yet." });
        }

        GeminiStylistResult result;
        try
        {
            result = await AskGeminiAsync(request.Message.Trim(), products, apiKey, cancellationToken);
        }
        catch (InvalidOperationException exception)
        {
            _logger.LogWarning(exception, "AI Stylist provider request failed.");
            return StatusCode(StatusCodes.Status502BadGateway, new { message = exception.Message });
        }

        var selectedProducts = SelectProducts(products, result.ProductIds);

        return Ok(new AiStylistResponseDto
        {
            OutfitName = result.OutfitName,
            Summary = result.Summary,
            Reasoning = result.Reasoning,
            StylingTips = result.StylingTips,
            ProductIds = selectedProducts.Select(product => product.Id).ToArray(),
            Products = selectedProducts.Select(ProductMapper.ToDetailDto).ToArray()
        });
    }

    private async Task<GeminiStylistResult> AskGeminiAsync(string userMessage, IReadOnlyCollection<Models.Product> products, string apiKey, CancellationToken cancellationToken)
    {
        var model = _configuration["Gemini:Model"] ?? "gemini-3.5-flash";
        var catalog = BuildCatalog(products);
        var prompt = $$"""
You are MAEVEN AI Stylist for a premium men's fashion ecommerce store.
Recommend a complete look using only products from this catalog.
Return strict JSON only. No markdown. No text before or after JSON.

JSON shape:
{
  "outfitName": "short outfit name",
  "summary": "2-3 sentence friendly recommendation",
  "reasoning": "why this works for the customer",
  "stylingTips": ["tip 1", "tip 2", "tip 3"],
  "productIds": ["p1", "p2", "p3"]
}

Customer request:
{{userMessage}}

Product catalog:
{{catalog}}
""";

        var payload = new
        {
            model,
            system_instruction = "You are a concise fashion stylist. Prefer practical ecommerce recommendations and only choose existing product ids from the catalog.",
            input = prompt,
            generation_config = new
            {
                temperature = 0.7,
                max_output_tokens = 800
            }
        };

        var client = _httpClientFactory.CreateClient();
        using var httpRequest = new HttpRequestMessage(HttpMethod.Post, "https://generativelanguage.googleapis.com/v1beta/interactions");
        httpRequest.Headers.Add("x-goog-api-key", apiKey);
        httpRequest.Content = JsonContent.Create(payload);

        using var response = await client.SendAsync(httpRequest, cancellationToken);
        var rawResponse = await response.Content.ReadAsStringAsync(cancellationToken);
        if (!response.IsSuccessStatusCode)
        {
            _logger.LogWarning("Gemini request failed with status {StatusCode}: {Body}", response.StatusCode, rawResponse);
            throw new InvalidOperationException("AI Stylist provider is unavailable right now. Please check the Gemini API key and model.");
        }

        var outputText = ExtractGeminiText(rawResponse);
        var parsed = TryParseStylistResult(outputText);
        return parsed ?? BuildFallbackResult(userMessage, products);
    }

    private static string BuildCatalog(IEnumerable<Models.Product> products)
    {
        var builder = new StringBuilder();
        foreach (var product in products)
        {
            builder.AppendLine($"- id: {product.Id}");
            builder.AppendLine($"  name: {product.Brand} {product.Name}");
            builder.AppendLine($"  price: {product.Price:0.##}");
            builder.AppendLine($"  category: {product.Category} / {product.Subcategory}");
            builder.AppendLine($"  colors: {string.Join(", ", product.Colors.Select(color => color.Value))}");
            builder.AppendLine($"  sizes: {string.Join(", ", product.Sizes.Select(size => size.Value))}");
            builder.AppendLine($"  tags: {string.Join(", ", product.Tags.Select(tag => tag.Value))}");
            builder.AppendLine($"  description: {product.Description}");
        }

        return builder.ToString();
    }

    private static string ExtractGeminiText(string rawResponse)
    {
        using var document = JsonDocument.Parse(rawResponse);
        var root = document.RootElement;

        if (root.TryGetProperty("output_text", out var outputText))
        {
            return outputText.GetString() ?? string.Empty;
        }

        if (root.TryGetProperty("output", out var output) && output.ValueKind == JsonValueKind.Array)
        {
            var texts = new List<string>();
            foreach (var item in output.EnumerateArray())
            {
                CollectText(item, texts);
            }

            if (texts.Count > 0)
            {
                return string.Join(Environment.NewLine, texts);
            }
        }

        return rawResponse;
    }

    private static void CollectText(JsonElement element, ICollection<string> texts)
    {
        if (element.ValueKind == JsonValueKind.Object)
        {
            foreach (var property in element.EnumerateObject())
            {
                if (property.Name.Equals("text", StringComparison.OrdinalIgnoreCase) && property.Value.ValueKind == JsonValueKind.String)
                {
                    texts.Add(property.Value.GetString() ?? string.Empty);
                }
                else
                {
                    CollectText(property.Value, texts);
                }
            }
        }
        else if (element.ValueKind == JsonValueKind.Array)
        {
            foreach (var item in element.EnumerateArray())
            {
                CollectText(item, texts);
            }
        }
    }

    private static GeminiStylistResult? TryParseStylistResult(string text)
    {
        var start = text.IndexOf('{');
        var end = text.LastIndexOf('}');
        if (start < 0 || end <= start)
        {
            return null;
        }

        var json = text[start..(end + 1)];
        try
        {
            return JsonSerializer.Deserialize<GeminiStylistResult>(json, JsonOptions);
        }
        catch (JsonException)
        {
            return null;
        }
    }

    private static GeminiStylistResult BuildFallbackResult(string userMessage, IReadOnlyCollection<Models.Product> products)
    {
        var lowerMessage = userMessage.ToLowerInvariant();
        var selected = products
            .OrderByDescending(product =>
                product.Tags.Count(tag => lowerMessage.Contains(tag.Value.ToLowerInvariant())) +
                (lowerMessage.Contains(product.Subcategory.ToLowerInvariant()) ? 2 : 0) +
                (product.IsBestSeller ? 1 : 0))
            .ThenByDescending(product => product.IsTrending)
            .Take(4)
            .Select(product => product.Id)
            .ToArray();

        return new GeminiStylistResult
        {
            OutfitName = "Curated MAEVEN Look",
            Summary = "I put together a polished look from the current MAEVEN catalog based on your request.",
            Reasoning = "These pieces balance versatility, fit, and occasion-readiness while staying easy to style together.",
            StylingTips = ["Keep the palette restrained.", "Let one tailored piece anchor the outfit.", "Finish with clean leather accessories."],
            ProductIds = selected
        };
    }

    private static List<Models.Product> SelectProducts(IReadOnlyCollection<Models.Product> products, IEnumerable<string> productIds)
    {
        var byId = products.ToDictionary(product => product.Id, StringComparer.OrdinalIgnoreCase);
        var selected = productIds
            .Where(byId.ContainsKey)
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .Take(4)
            .Select(productId => byId[productId])
            .ToList();

        if (selected.Count > 0)
        {
            return selected;
        }

        return products
            .OrderByDescending(product => product.IsBestSeller)
            .ThenByDescending(product => product.Rating)
            .Take(4)
            .ToList();
    }
}
