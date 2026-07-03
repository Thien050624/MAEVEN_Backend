using MAEVEN.Backend.Data;
using MAEVEN.Backend.Dtos;
using MAEVEN.Backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MAEVEN.Backend.Controllers;

[ApiController]
[Route("api/products")]
public class ProductsController : ControllerBase
{
    private readonly AppDbContext _dbContext;

    public ProductsController(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductDetailDto>>> GetAll([FromQuery] string? category = null, [FromQuery] string? search = null)
    {
        IQueryable<Product> query = _dbContext.Products
            .AsNoTracking()
            .Include(product => product.Images)
            .Include(product => product.Colors)
            .Include(product => product.Sizes)
            .Include(product => product.Tags)
            .Include(product => product.Specifications)
            .Include(product => product.Reviews)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(category))
        {
            query = query.Where(product => product.Category == category);
        }

        if (!string.IsNullOrWhiteSpace(search))
        {
            query = query.Where(product =>
                EF.Functions.ILike(product.Name, $"%{search}%") ||
                EF.Functions.ILike(product.Brand, $"%{search}%") ||
                EF.Functions.ILike(product.Subcategory, $"%{search}%") ||
                EF.Functions.ILike(product.Description, $"%{search}%"));
        }

        var products = await query
            .OrderBy(product => product.Id)
            .ToListAsync();

        return Ok(products.Select(ProductMapper.ToDetailDto));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductDetailDto>> GetById(string id)
    {
        var product = await _dbContext.Products
            .AsNoTracking()
            .Include(item => item.Images)
            .Include(item => item.Colors)
            .Include(item => item.Sizes)
            .Include(item => item.Tags)
            .Include(item => item.Specifications)
            .Include(item => item.Reviews)
            .FirstOrDefaultAsync(item => item.Id == id);

        if (product is null)
        {
            return NotFound();
        }

        return Ok(ProductMapper.ToDetailDto(product));
    }

    [Authorize(Roles = "admin")]
    [HttpPost]
    public async Task<ActionResult<ProductDetailDto>> Create(ProductUpsertDto dto)
    {
        if (await _dbContext.Products.AnyAsync(product => product.Id == dto.Id))
        {
            return Conflict(new { message = "Product id already exists." });
        }

        var product = new Product
        {
            Id = dto.Id,
            Name = dto.Name,
            Brand = dto.Brand,
            Price = dto.Price,
            OriginalPrice = dto.OriginalPrice,
            Discount = dto.Discount,
            Rating = dto.Rating,
            ReviewsCount = dto.ReviewsCount,
            Category = dto.Category,
            Subcategory = dto.Subcategory,
            Description = dto.Description,
            IsNew = dto.IsNew,
            IsBestSeller = dto.IsBestSeller,
            IsTrending = dto.IsTrending,
            IsLimited = dto.IsLimited,
            InStock = dto.InStock,
            Colors = dto.Colors.Select(value => new ProductColor { Value = value }).ToList(),
            Sizes = dto.Sizes.Select(value => new ProductSize { Value = value }).ToList(),
            Tags = dto.Tags.Select(value => new ProductTag { Value = value }).ToList(),
            Images = dto.Images.Select((value, index) => new ProductImage { Url = value, SortOrder = index + 1 }).ToList(),
            Specifications = dto.Specs.Select(pair => new ProductSpecification { Name = pair.Key, Value = pair.Value }).ToList()
        };

        _dbContext.Products.Add(product);
        await _dbContext.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = product.Id }, ProductMapper.ToDetailDto(product));
    }

    [Authorize(Roles = "admin")]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, ProductUpsertDto dto)
    {
        var product = await _dbContext.Products
            .Include(item => item.Images)
            .Include(item => item.Colors)
            .Include(item => item.Sizes)
            .Include(item => item.Tags)
            .Include(item => item.Specifications)
            .FirstOrDefaultAsync(item => item.Id == id);

        if (product is null)
        {
            return NotFound();
        }

        product.Name = dto.Name;
        product.Brand = dto.Brand;
        product.Price = dto.Price;
        product.OriginalPrice = dto.OriginalPrice;
        product.Discount = dto.Discount;
        product.Rating = dto.Rating;
        product.ReviewsCount = dto.ReviewsCount;
        product.Category = dto.Category;
        product.Subcategory = dto.Subcategory;
        product.Description = dto.Description;
        product.IsNew = dto.IsNew;
        product.IsBestSeller = dto.IsBestSeller;
        product.IsTrending = dto.IsTrending;
        product.IsLimited = dto.IsLimited;
        product.InStock = dto.InStock;

        _dbContext.ProductImages.RemoveRange(product.Images);
        _dbContext.ProductColors.RemoveRange(product.Colors);
        _dbContext.ProductSizes.RemoveRange(product.Sizes);
        _dbContext.ProductTags.RemoveRange(product.Tags);
        _dbContext.ProductSpecifications.RemoveRange(product.Specifications);

        product.Images = dto.Images.Select((value, index) => new ProductImage { ProductId = product.Id, Url = value, SortOrder = index + 1 }).ToList();
        product.Colors = dto.Colors.Select(value => new ProductColor { ProductId = product.Id, Value = value }).ToList();
        product.Sizes = dto.Sizes.Select(value => new ProductSize { ProductId = product.Id, Value = value }).ToList();
        product.Tags = dto.Tags.Select(value => new ProductTag { ProductId = product.Id, Value = value }).ToList();
        product.Specifications = dto.Specs.Select(pair => new ProductSpecification { ProductId = product.Id, Name = pair.Key, Value = pair.Value }).ToList();

        await _dbContext.SaveChangesAsync();
        return NoContent();
    }

    [Authorize(Roles = "admin")]
    [HttpPatch("{id}/sale")]
    public async Task<IActionResult> UpdateSale(string id, [FromBody] UpdateSaleRequest request)
    {
        var product = await _dbContext.Products.FirstOrDefaultAsync(item => item.Id == id);

        if (product is null)
        {
            return NotFound();
        }

        var basePrice = product.OriginalPrice ?? product.Price;

        if (request.Discount is null || request.Discount <= 0)
        {
            product.Price = basePrice;
            product.OriginalPrice = null;
            product.Discount = null;
        }
        else
        {
            product.OriginalPrice = basePrice;
            product.Discount = request.Discount;
            product.Price = Math.Round(basePrice * (1 - request.Discount.Value / 100m), 2);
        }

        await _dbContext.SaveChangesAsync();
        return NoContent();
    }

    [Authorize(Roles = "admin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var product = await _dbContext.Products.FirstOrDefaultAsync(item => item.Id == id);

        if (product is null)
        {
            return NotFound();
        }

        _dbContext.Products.Remove(product);
        await _dbContext.SaveChangesAsync();

        return NoContent();
    }

}

public record UpdateSaleRequest(int? Discount);
