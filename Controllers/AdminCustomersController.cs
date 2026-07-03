using MAEVEN.Backend.Data;
using MAEVEN.Backend.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MAEVEN.Backend.Controllers;

[ApiController]
[Authorize(Roles = "admin")]
[Route("api/admin/customers")]
public class AdminCustomersController : ControllerBase
{
    private readonly AppDbContext _dbContext;

    public AdminCustomersController(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AdminCustomerDto>>> GetAll([FromQuery] string? search = null)
    {
        var query = _dbContext.Users
            .AsNoTracking()
            .Include(user => user.Orders)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
        {
            var normalizedSearch = search.Trim();
            query = query.Where(user =>
                EF.Functions.ILike(user.Name, $"%{normalizedSearch}%") ||
                EF.Functions.ILike(user.Email, $"%{normalizedSearch}%") ||
                EF.Functions.ILike(user.Id, $"%{normalizedSearch}%"));
        }

        var customers = await query
            .OrderByDescending(user => user.CreatedAt)
            .Select(user => new AdminCustomerDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Avatar = user.Avatar,
                Role = user.Role,
                Tier = user.Tier,
                CreatedAt = user.CreatedAt,
                OrdersCount = user.Orders.Count,
                TotalSpent = user.Orders.Sum(order => order.Total),
                LastOrderAt = user.Orders
                    .OrderByDescending(order => order.CreatedAt)
                    .Select(order => (DateTime?)order.CreatedAt)
                    .FirstOrDefault()
            })
            .ToListAsync();

        return Ok(customers);
    }
}
