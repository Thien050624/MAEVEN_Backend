namespace MAEVEN.Backend.Dtos;

public class ProductListItemDto
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Brand { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public decimal? OriginalPrice { get; set; }
    public int? Discount { get; set; }
    public decimal Rating { get; set; }
    public int Reviews { get; set; }
    public string Category { get; set; } = string.Empty;
    public string Subcategory { get; set; } = string.Empty;
    public string[] Colors { get; set; } = [];
    public string[] Sizes { get; set; } = [];
    public string Image { get; set; } = string.Empty;
    public bool IsNew { get; set; }
    public bool IsBestSeller { get; set; }
    public bool IsTrending { get; set; }
    public bool IsLimited { get; set; }
    public bool InStock { get; set; }
}

public class ProductDetailDto : ProductListItemDto
{
    public string Description { get; set; } = string.Empty;
    public string[] Images { get; set; } = [];
    public Dictionary<string, string> Specs { get; set; } = [];
    public string[] Tags { get; set; } = [];
    public ReviewDto[] ReviewsData { get; set; } = [];
}

public class ReviewDto
{
    public int Id { get; set; }
    public string ProductId { get; set; } = string.Empty;
    public string User { get; set; } = string.Empty;
    public string Avatar { get; set; } = string.Empty;
    public int Rating { get; set; }
    public DateOnly Date { get; set; }
    public string Comment { get; set; } = string.Empty;
    public int Helpful { get; set; }
}

public class ProductUpsertDto
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
    public string[] Colors { get; set; } = [];
    public string[] Sizes { get; set; } = [];
    public string[] Images { get; set; } = [];
    public Dictionary<string, string> Specs { get; set; } = [];
    public string[] Tags { get; set; } = [];
}