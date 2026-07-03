namespace MAEVEN.Backend.Models;

public class ProductSpecification
{
    public int Id { get; set; }
    public string ProductId { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Value { get; set; } = string.Empty;

    public Product Product { get; set; } = null!;
}