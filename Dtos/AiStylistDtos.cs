namespace MAEVEN.Backend.Dtos;

public class AiStylistRequestDto
{
    public string Message { get; set; } = string.Empty;
}

public class AiStylistResponseDto
{
    public string OutfitName { get; set; } = string.Empty;
    public string Summary { get; set; } = string.Empty;
    public string Reasoning { get; set; } = string.Empty;
    public string[] StylingTips { get; set; } = [];
    public string[] ProductIds { get; set; } = [];
    public ProductDetailDto[] Products { get; set; } = [];
}

public class GeminiStylistResult
{
    public string OutfitName { get; set; } = string.Empty;
    public string Summary { get; set; } = string.Empty;
    public string Reasoning { get; set; } = string.Empty;
    public string[] StylingTips { get; set; } = [];
    public string[] ProductIds { get; set; } = [];
}
