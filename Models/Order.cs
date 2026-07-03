namespace MAEVEN.Backend.Models;

public class Order
{
    public string Id { get; set; } = $"MAE-{DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()}";
    public string UserId { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public string Status { get; set; } = "Processing";
    public string PaymentMethod { get; set; } = string.Empty;
    public string PaymentStatus { get; set; } = "Pending";
    public string DeliveryMethod { get; set; } = string.Empty;
    public decimal Subtotal { get; set; }
    public decimal ShippingCost { get; set; }
    public decimal Tax { get; set; }
    public decimal Total { get; set; }

    public User User { get; set; } = null!;
    public ShippingAddress ShippingAddress { get; set; } = null!;
    public ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();
}
