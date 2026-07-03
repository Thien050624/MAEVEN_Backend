using MAEVEN.Backend.Models;

namespace MAEVEN.Backend.Dtos;

public static class ProductMapper
{
    public static ProductDetailDto ToDetailDto(Product product)
    {
        return new ProductDetailDto
        {
            Id = product.Id,
            Name = product.Name,
            Brand = product.Brand,
            Price = product.Price,
            OriginalPrice = product.OriginalPrice,
            Discount = product.Discount,
            Rating = product.Rating,
            Reviews = product.ReviewsCount,
            Category = product.Category,
            Subcategory = product.Subcategory,
            Colors = product.Colors.Select(color => color.Value).ToArray(),
            Sizes = product.Sizes.Select(size => size.Value).ToArray(),
            Image = product.Images.OrderBy(image => image.SortOrder).Select(image => image.Url).FirstOrDefault() ?? string.Empty,
            IsNew = product.IsNew,
            IsBestSeller = product.IsBestSeller,
            IsTrending = product.IsTrending,
            IsLimited = product.IsLimited,
            InStock = product.InStock,
            Description = product.Description,
            Images = product.Images.OrderBy(image => image.SortOrder).Select(image => image.Url).ToArray(),
            Specs = product.Specifications.ToDictionary(specification => specification.Name, specification => specification.Value),
            Tags = product.Tags.Select(tag => tag.Value).ToArray(),
            ReviewsData = product.Reviews
                .OrderByDescending(review => review.Date)
                .Select(review => new ReviewDto
                {
                    Id = review.Id,
                    ProductId = review.ProductId,
                    User = review.User,
                    Avatar = review.AvatarUrl,
                    Rating = review.Rating,
                    Date = review.Date,
                    Comment = review.Comment,
                    Helpful = review.Helpful
                })
                .ToArray()
        };
    }
}