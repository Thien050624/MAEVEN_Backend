using System.Security.Claims;
using MAEVEN.Backend.Data;
using MAEVEN.Backend.Dtos;
using MAEVEN.Backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MAEVEN.Backend.Controllers;

[ApiController]
[Authorize]
[Route("api/orders")]
public class OrdersController : ControllerBase
{
    private readonly AppDbContext _dbContext;

    public OrdersController(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpPost]
    public async Task<ActionResult<OrderDto>> Create(CreateOrderDto dto)
    {
        var userId = GetUserId();
        if (userId is null)
        {
            return Unauthorized();
        }

        var productIds = dto.Items.Select(item => item.ProductId).Distinct().ToArray();
        var products = await _dbContext.Products
            .AsNoTracking()
            .Include(product => product.Images)
            .Where(product => productIds.Contains(product.Id))
            .ToDictionaryAsync(product => product.Id);

        if (products.Count != productIds.Length)
        {
            return BadRequest(new { message = "One or more products are invalid." });
        }

        var orderItems = dto.Items.Select(item =>
        {
            var product = products[item.ProductId];
            return new OrderItem
            {
                ProductId = product.Id,
                ProductName = product.Name,
                ProductImage = product.Images.OrderBy(image => image.SortOrder).Select(image => image.Url).FirstOrDefault() ?? string.Empty,
                UnitPrice = product.Price,
                Quantity = item.Quantity,
                Size = item.Size,
                Color = item.Color
            };
        }).ToList();

        var subtotal = orderItems.Sum(item => item.UnitPrice * item.Quantity);
        var shippingCost = GetShippingCost(dto.DeliveryMethod, subtotal);
        var tax = Math.Round(subtotal * 0.08m, 2);
        var total = subtotal + shippingCost + tax;

        var order = new Order
        {
            Id = $"MAE-{DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()}",
            UserId = userId,
            Status = "Processing",
            PaymentMethod = NormalizePaymentMethod(dto.PaymentMethod),
            PaymentStatus = dto.PaymentMethod == "cod" ? "Pending" : "AwaitingTransfer",
            DeliveryMethod = NormalizeDeliveryMethod(dto.DeliveryMethod),
            Subtotal = subtotal,
            ShippingCost = shippingCost,
            Tax = tax,
            Total = total,
            ShippingAddress = new ShippingAddress
            {
                FirstName = dto.ShippingAddress.FirstName,
                LastName = dto.ShippingAddress.LastName,
                Email = dto.ShippingAddress.Email,
                Phone = dto.ShippingAddress.Phone,
                Address = dto.ShippingAddress.Address,
                Country = string.IsNullOrWhiteSpace(dto.ShippingAddress.Country) ? "Vietnam" : dto.ShippingAddress.Country
            },
            Items = orderItems
        };

        _dbContext.Orders.Add(order);
        await _dbContext.SaveChangesAsync();

        var createdOrder = await QueryOrders()
            .FirstAsync(item => item.Id == order.Id);

        return CreatedAtAction(nameof(GetById), new { id = order.Id }, OrderMapper.ToDto(createdOrder));
    }

    [HttpGet("me")]
    public async Task<ActionResult<IEnumerable<OrderDto>>> GetMine()
    {
        var userId = GetUserId();
        if (userId is null)
        {
            return Unauthorized();
        }

        var orders = await QueryOrders()
            .Where(order => order.UserId == userId)
            .OrderByDescending(order => order.CreatedAt)
            .ToListAsync();

        return Ok(orders.Select(OrderMapper.ToDto));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<OrderDto>> GetById(string id)
    {
        var userId = GetUserId();
        if (userId is null)
        {
            return Unauthorized();
        }

        var isAdmin = User.IsInRole("admin");
        var order = await QueryOrders()
            .FirstOrDefaultAsync(item => item.Id == id && (isAdmin || item.UserId == userId));

        if (order is null)
        {
            return NotFound();
        }

        return Ok(OrderMapper.ToDto(order));
    }

    [Authorize(Roles = "admin")]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<OrderDto>>> GetAll()
    {
        var orders = await QueryOrders()
            .OrderByDescending(order => order.CreatedAt)
            .ToListAsync();

        return Ok(orders.Select(OrderMapper.ToDto));
    }

    private IQueryable<Order> QueryOrders()
    {
        return _dbContext.Orders
            .AsNoTracking()
            .Include(order => order.User)
            .Include(order => order.ShippingAddress)
            .Include(order => order.Items);
    }

    private string? GetUserId()
    {
        return User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    }

    private static decimal GetShippingCost(string deliveryMethod, decimal subtotal)
    {
        return deliveryMethod switch
        {
            "express" => 15m,
            "same-day" => 30m,
            _ => subtotal >= 150m ? 0m : 8m
        };
    }

    private static string NormalizePaymentMethod(string paymentMethod)
    {
        return paymentMethod == "cod" ? "cod" : "qr";
    }

    private static string NormalizeDeliveryMethod(string deliveryMethod)
    {
        return deliveryMethod switch
        {
            "express" => "express",
            "same-day" => "same-day",
            _ => "standard"
        };
    }
}
