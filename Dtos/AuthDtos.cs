using System.ComponentModel.DataAnnotations;

namespace MAEVEN.Backend.Dtos;

public record RegisterRequestDto(
    [Required][EmailAddress] string Email,
    [Required][MinLength(6)] string Password,
    [Required] string Name
);

public record LoginRequestDto(
    [Required][EmailAddress] string Email,
    [Required] string Password
);

public record AuthResponseDto(
    string Token,
    UserDto User
);

public record UserDto(
    string Id,
    string Email,
    string Name,
    string Avatar,
    string Role,
    string Tier
);

public record UpdateProfileRequestDto(
    [Required] string Name,
    string? Avatar
);
