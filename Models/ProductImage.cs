namespace MAEVEN.Backend.Models;

public class ProductImage
{
    public int Id { get; set; }
    public string ProductId { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public int SortOrder { get; set; }

    public Product Product { get; set; } = null!;
}