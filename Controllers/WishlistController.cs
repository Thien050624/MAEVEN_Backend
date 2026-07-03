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
[Route("api/wishlist")]
public class WishlistController : ControllerBase
{
    private readonly AppDbContext _dbContext;

    public WishlistController(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductDetailDto>>> GetAll()
    {
        var userId = GetUserId();
        if (userId is null)
        {
            return Unauthorized();
        }

        var products = await _dbContext.WishlistItems
            .AsNoTracking()
            .Where(item => item.UserId == userId)
            .Include(item => item.Product)
                .ThenInclude(product => product.Images)
            .Include(item => item.Product)
                .ThenInclude(product => product.Colors)
            .Include(item => item.Product)
                .ThenInclude(product => product.Sizes)
            .Include(item => item.Product)
                .ThenInclude(product => product.Tags)
            .Include(item => item.Product)
                .ThenInclude(product => product.Specifications)
            .Include(item => item.Product)
                .ThenInclude(product => product.Reviews)
            .Select(item => item.Product)
            .ToListAsync();

        return Ok(products.Select(ProductMapper.ToDetailDto));
    }

    [HttpPost("{productId}")]
    public async Task<IActionResult> Add(string productId)
    {
        var userId = GetUserId();
        if (userId is null)
        {
            return Unauthorized();
        }

        var exists = await _dbContext.Products.AnyAsync(product => product.Id == productId);
        if (!exists)
        {
            return NotFound();
        }

        if (!await _dbContext.WishlistItems.AnyAsync(item => item.UserId == userId && item.ProductId == productId))
        {
            _dbContext.WishlistItems.Add(new WishlistItem
            {
                UserId = userId,
                ProductId = productId,
                CreatedAt = DateTime.UtcNow
            });
            await _dbContext.SaveChangesAsync();
        }

        return NoContent();
    }

    [HttpDelete("{productId}")]
    public async Task<IActionResult> Remove(string productId)
    {
        var userId = GetUserId();
        if (userId is null)
        {
            return Unauthorized();
        }

        var item = await _dbContext.WishlistItems.FirstOrDefaultAsync(item => item.UserId == userId && item.ProductId == productId);
        if (item is null)
        {
            return NotFound();
        }

        _dbContext.WishlistItems.Remove(item);
        await _dbContext.SaveChangesAsync();
        return NoContent();
    }

    private string? GetUserId()
    {
        return User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    }
}
