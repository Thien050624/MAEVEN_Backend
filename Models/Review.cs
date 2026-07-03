namespace MAEVEN.Backend.Models;

public class Review
{
    public int Id { get; set; }
    public string ProductId { get; set; } = string.Empty;
    public string User { get; set; } = string.Empty;
    public string AvatarUrl { get; set; } = string.Empty;
    public int Rating { get; set; }
    public DateOnly Date { get; set; }
    public string Comment { get; set; } = string.Empty;
    public int Helpful { get; set; }

    public Product Product { get; set; } = null!;
}