namespace MAEVEN.Backend.Dtos;

public class AdminCustomerDto
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Avatar { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public string Tier { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public int OrdersCount { get; set; }
    public decimal TotalSpent { get; set; }
    public DateTime? LastOrderAt { get; set; }
}
