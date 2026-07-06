using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text;
using System.Text.Json.Serialization;
using MAEVEN.Backend.Data;
using MAEVEN.Backend.Dtos;
using MAEVEN.Backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace MAEVEN.Backend.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly AppDbContext _dbContext;
    private readonly IConfiguration _configuration;
    private readonly IHttpClientFactory _httpClientFactory;

    public AuthController(AppDbContext dbContext, IConfiguration configuration, IHttpClientFactory httpClientFactory)
    {
        _dbContext = dbContext;
        _configuration = configuration;
        _httpClientFactory = httpClientFactory;
    }

    [HttpPost("register")]
    public async Task<ActionResult<AuthResponseDto>> Register(RegisterRequestDto dto)
    {
        if (await _dbContext.Users.AnyAsync(u => u.Email.ToLower() == dto.Email.ToLower()))
        {
            return Conflict(new { message = "Email already registered." });
        }

        var passwordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);

        var user = new User
        {
            Email = dto.Email.ToLower(),
            PasswordHash = passwordHash,
            Name = dto.Name,
            Role = "user",
            Tier = "Silver"
        };

        _dbContext.Users.Add(user);
        await _dbContext.SaveChangesAsync();

        var token = GenerateJwtToken(user);

        var userDto = new UserDto(user.Id, user.Email, user.Name, user.Avatar, user.Role, user.Tier);
        return Ok(new AuthResponseDto(token, userDto));
    }

    [HttpPost("login")]
    public async Task<ActionResult<AuthResponseDto>> Login(LoginRequestDto dto)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email.ToLower() == dto.Email.ToLower());
        if (user is null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
        {
            return Unauthorized(new { message = "Invalid email or password." });
        }

        var token = GenerateJwtToken(user);

        var userDto = new UserDto(user.Id, user.Email, user.Name, user.Avatar, user.Role, user.Tier);
        return Ok(new AuthResponseDto(token, userDto));
    }

    [HttpPost("google")]
    public async Task<ActionResult<AuthResponseDto>> GoogleLogin(GoogleLoginRequestDto dto)
    {
        var clientId = _configuration["GoogleAuth:ClientId"];
        if (string.IsNullOrWhiteSpace(clientId))
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Google login is not configured." });
        }

        var httpClient = _httpClientFactory.CreateClient();
        var tokenInfo = await httpClient.GetFromJsonAsync<GoogleTokenInfo>(
            $"https://oauth2.googleapis.com/tokeninfo?id_token={Uri.EscapeDataString(dto.IdToken)}");

        if (tokenInfo is null ||
            tokenInfo.Audience != clientId ||
            string.IsNullOrWhiteSpace(tokenInfo.Email) ||
            !string.Equals(tokenInfo.EmailVerified, "true", StringComparison.OrdinalIgnoreCase))
        {
            return Unauthorized(new { message = "Invalid Google account." });
        }

        var normalizedEmail = tokenInfo.Email.Trim().ToLowerInvariant();
        var user = await _dbContext.Users.FirstOrDefaultAsync(item => item.Email == normalizedEmail);

        if (user is null)
        {
            user = new User
            {
                Email = normalizedEmail,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(Guid.NewGuid().ToString("N")),
                Name = string.IsNullOrWhiteSpace(tokenInfo.Name) ? normalizedEmail.Split('@')[0] : tokenInfo.Name.Trim(),
                Avatar = tokenInfo.Picture ?? string.Empty,
                Role = "user",
                Tier = "Silver"
            };

            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();
        }
        else
        {
            var changed = false;
            if (string.IsNullOrWhiteSpace(user.Avatar) && !string.IsNullOrWhiteSpace(tokenInfo.Picture))
            {
                user.Avatar = tokenInfo.Picture;
                changed = true;
            }

            if (changed)
            {
                await _dbContext.SaveChangesAsync();
            }
        }

        var token = GenerateJwtToken(user);
        var userDto = new UserDto(user.Id, user.Email, user.Name, user.Avatar, user.Role, user.Tier);

        return Ok(new AuthResponseDto(token, userDto));
    }

    [Authorize]
    [HttpPost("logout")]
    public IActionResult Logout()
    {
        return Ok(new { message = "Logged out successfully." });
    }

    [Authorize]
    [HttpGet("me")]
    public async Task<ActionResult<UserDto>> GetMe()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized();
        }

        var user = await _dbContext.Users.FindAsync(userId);
        if (user is null)
        {
            return NotFound(new { message = "User not found." });
        }

        return Ok(new UserDto(user.Id, user.Email, user.Name, user.Avatar, user.Role, user.Tier));
    }

    [Authorize]
    [HttpPut("me")]
    public async Task<ActionResult<UserDto>> UpdateMe(UpdateProfileRequestDto dto)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized();
        }

        var user = await _dbContext.Users.FindAsync(userId);
        if (user is null)
        {
            return NotFound(new { message = "User not found." });
        }

        if (string.IsNullOrWhiteSpace(dto.Name))
        {
            return BadRequest(new { message = "Name is required." });
        }

        user.Name = dto.Name.Trim();
        user.Avatar = dto.Avatar?.Trim() ?? string.Empty;
        await _dbContext.SaveChangesAsync();

        return Ok(new UserDto(user.Id, user.Email, user.Name, user.Avatar, user.Role, user.Tier));
    }

    private string GenerateJwtToken(User user)
    {
        var jwtSettings = _configuration.GetSection("JwtSettings");
        var secretKey = jwtSettings["Secret"]!;
        var issuer = jwtSettings["Issuer"]!;
        var audience = jwtSettings["Audience"]!;
        var expireDays = double.Parse(jwtSettings["ExpireDays"] ?? "7");

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Name, user.Name),
            new Claim(ClaimTypes.Role, user.Role)
        };

        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: DateTime.UtcNow.AddDays(expireDays),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}

public sealed class GoogleTokenInfo
{
    [JsonPropertyName("aud")]
    public string? Audience { get; set; }

    [JsonPropertyName("email")]
    public string? Email { get; set; }

    [JsonPropertyName("email_verified")]
    public string? EmailVerified { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("picture")]
    public string? Picture { get; set; }
}
