using System.ComponentModel.DataAnnotations;

namespace MAEVEN.Backend.Models;

public class User
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N");

    [Required]
    [EmailAddress]
    [MaxLength(150)]
    public string Email { get; set; } = string.Empty;

    [Required]
    [MaxLength(200)]
    public string PasswordHash { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    public string Avatar { get; set; } = string.Empty;

    [Required]
    [MaxLength(20)]
    public string Role { get; set; } = "user"; // "user" | "admin"

    [Required]
    [MaxLength(20)]
    public string Tier { get; set; } = "Silver"; // "Silver" | "Gold" | "Platinum"

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<WishlistItem> WishlistItems { get; set; } = new List<WishlistItem>();
    public ICollection<Order> Orders { get; set; } = new List<Order>();
}
