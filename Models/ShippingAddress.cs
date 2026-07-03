namespace MAEVEN.Backend.Models;

public class ShippingAddress
{
    public int Id { get; set; }
    public string OrderId { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string Country { get; set; } = "Vietnam";

    public Order Order { get; set; } = null!;
}
