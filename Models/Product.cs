namespace MAEVEN.Backend.Models;

public class Product
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Brand { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public decimal? OriginalPrice { get; set; }
    public int? Discount { get; set; }
    public decimal Rating { get; set; }
    public int ReviewsCount { get; set; }
    public string Category { get; set; } = string.Empty;
    public string Subcategory { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public bool IsNew { get; set; }
    public bool IsBestSeller { get; set; }
    public bool IsTrending { get; set; }
    public bool IsLimited { get; set; }
    public bool InStock { get; set; }

    public ICollection<ProductImage> Images { get; set; } = new List<ProductImage>();
    public ICollection<ProductColor> Colors { get; set; } = new List<ProductColor>();
    public ICollection<ProductSize> Sizes { get; set; } = new List<ProductSize>();
    public ICollection<ProductTag> Tags { get; set; } = new List<ProductTag>();
    public ICollection<ProductSpecification> Specifications { get; set; } = new List<ProductSpecification>();
    public ICollection<Review> Reviews { get; set; } = new List<Review>();
    public ICollection<WishlistItem> WishlistItems { get; set; } = new List<WishlistItem>();
}
