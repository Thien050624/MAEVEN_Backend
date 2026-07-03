using System.ComponentModel.DataAnnotations;
using MAEVEN.Backend.Models;

namespace MAEVEN.Backend.Dtos;

public class CreateOrderDto
{
    [Required]
    public ShippingAddressDto ShippingAddress { get; set; } = new();

    [Required]
    public string DeliveryMethod { get; set; } = string.Empty;

    [Required]
    public string PaymentMethod { get; set; } = string.Empty;

    [Required]
    [MinLength(1)]
    public List<CreateOrderItemDto> Items { get; set; } = [];
}

public class CreateOrderItemDto
{
    [Required]
    public string ProductId { get; set; } = string.Empty;

    [Range(1, 99)]
    public int Quantity { get; set; }

    [Required]
    public string Size { get; set; } = string.Empty;

    [Required]
    public string Color { get; set; } = string.Empty;
}

public class ShippingAddressDto
{
    [Required]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    public string LastName { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string Phone { get; set; } = string.Empty;

    [Required]
    public string Address { get; set; } = string.Empty;

    public string Country { get; set; } = "Vietnam";
}

public class OrderDto
{
    public string Id { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public string CustomerName { get; set; } = string.Empty;
    public string CustomerEmail { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public string Status { get; set; } = string.Empty;
    public string PaymentMethod { get; set; } = string.Empty;
    public string PaymentStatus { get; set; } = string.Empty;
    public string DeliveryMethod { get; set; } = string.Empty;
    public decimal Subtotal { get; set; }
    public decimal ShippingCost { get; set; }
    public decimal Tax { get; set; }
    public decimal Total { get; set; }
    public ShippingAddressDto ShippingAddress { get; set; } = new();
    public List<OrderItemDto> Items { get; set; } = [];
}

public class UpdateOrderStatusDto
{
    [Required]
    public string Status { get; set; } = string.Empty;

    [Required]
    public string PaymentStatus { get; set; } = string.Empty;
}

public class OrderItemDto
{
    public string ProductId { get; set; } = string.Empty;
    public string ProductName { get; set; } = string.Empty;
    public string ProductImage { get; set; } = string.Empty;
    public decimal UnitPrice { get; set; }
    public int Quantity { get; set; }
    public string Size { get; set; } = string.Empty;
    public string Color { get; set; } = string.Empty;
}

public static class OrderMapper
{
    public static OrderDto ToDto(Order order)
    {
        return new OrderDto
        {
            Id = order.Id,
            UserId = order.UserId,
            CustomerName = order.User.Name,
            CustomerEmail = order.User.Email,
            CreatedAt = order.CreatedAt,
            Status = order.Status,
            PaymentMethod = order.PaymentMethod,
            PaymentStatus = order.PaymentStatus,
            DeliveryMethod = order.DeliveryMethod,
            Subtotal = order.Subtotal,
            ShippingCost = order.ShippingCost,
            Tax = order.Tax,
            Total = order.Total,
            ShippingAddress = new ShippingAddressDto
            {
                FirstName = order.ShippingAddress.FirstName,
                LastName = order.ShippingAddress.LastName,
                Email = order.ShippingAddress.Email,
                Phone = order.ShippingAddress.Phone,
                Address = order.ShippingAddress.Address,
                Country = order.ShippingAddress.Country
            },
            Items = order.Items.Select(item => new OrderItemDto
            {
                ProductId = item.ProductId,
                ProductName = item.ProductName,
                ProductImage = item.ProductImage,
                UnitPrice = item.UnitPrice,
                Quantity = item.Quantity,
                Size = item.Size,
                Color = item.Color
            }).ToList()
        };
    }
}
