using MAEVEN.Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace MAEVEN.Backend.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Product> Products => Set<Product>();
    public DbSet<ProductImage> ProductImages => Set<ProductImage>();
    public DbSet<ProductColor> ProductColors => Set<ProductColor>();
    public DbSet<ProductSize> ProductSizes => Set<ProductSize>();
    public DbSet<ProductTag> ProductTags => Set<ProductTag>();
    public DbSet<ProductSpecification> ProductSpecifications => Set<ProductSpecification>();
    public DbSet<Review> Reviews => Set<Review>();
    public DbSet<WishlistItem> WishlistItems => Set<WishlistItem>();
    public DbSet<User> Users => Set<User>();
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<OrderItem> OrderItems => Set<OrderItem>();
    public DbSet<ShippingAddress> ShippingAddresses => Set<ShippingAddress>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>(entity =>
        {
            entity.ToTable("Products");
            entity.HasKey(product => product.Id);

            entity.Property(product => product.Id)
                .HasMaxLength(32)
                .IsRequired();

            entity.Property(product => product.Name)
                .HasMaxLength(200)
                .IsRequired();

            entity.Property(product => product.Brand)
                .HasMaxLength(120)
                .IsRequired();

            entity.Property(product => product.Price)
                .HasPrecision(18, 2);

            entity.Property(product => product.OriginalPrice)
                .HasPrecision(18, 2);

            entity.Property(product => product.Category)
                .HasMaxLength(50)
                .IsRequired();

            entity.Property(product => product.Subcategory)
                .HasMaxLength(100)
                .IsRequired();

            entity.Property(product => product.Description)
                .HasColumnType("text")
                .IsRequired();

            entity.HasMany(product => product.Images)
                .WithOne(image => image.Product)
                .HasForeignKey(image => image.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasMany(product => product.Colors)
                .WithOne(color => color.Product)
                .HasForeignKey(color => color.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasMany(product => product.Sizes)
                .WithOne(size => size.Product)
                .HasForeignKey(size => size.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasMany(product => product.Tags)
                .WithOne(tag => tag.Product)
                .HasForeignKey(tag => tag.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasMany(product => product.Specifications)
                .WithOne(specification => specification.Product)
                .HasForeignKey(specification => specification.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasMany(product => product.Reviews)
                .WithOne(review => review.Product)
                .HasForeignKey(review => review.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<ProductImage>(entity =>
        {
            entity.ToTable("ProductImages");
            entity.HasKey(image => image.Id);
            entity.Property(image => image.Url).HasColumnType("text").IsRequired();
        });

        modelBuilder.Entity<ProductColor>(entity =>
        {
            entity.ToTable("ProductColors");
            entity.HasKey(color => color.Id);
            entity.Property(color => color.Value)
                .HasMaxLength(32)
                .IsRequired();
        });

        modelBuilder.Entity<ProductSize>(entity =>
        {
            entity.ToTable("ProductSizes");
            entity.HasKey(size => size.Id);
            entity.Property(size => size.Value)
                .HasMaxLength(32)
                .IsRequired();
        });

        modelBuilder.Entity<ProductTag>(entity =>
        {
            entity.ToTable("ProductTags");
            entity.HasKey(tag => tag.Id);
            entity.Property(tag => tag.Value)
                .HasMaxLength(64)
                .IsRequired();
        });

        modelBuilder.Entity<ProductSpecification>(entity =>
        {
            entity.ToTable("ProductSpecifications");
            entity.HasKey(specification => specification.Id);
            entity.Property(specification => specification.Name)
                .HasMaxLength(120)
                .IsRequired();
            entity.Property(specification => specification.Value)
                .HasMaxLength(200)
                .IsRequired();
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.ToTable("Reviews");
            entity.HasKey(review => review.Id);
            entity.Property(review => review.User)
                .HasMaxLength(120)
                .IsRequired();
            entity.Property(review => review.AvatarUrl)
                .HasColumnType("text")
                .IsRequired();
            entity.Property(review => review.Comment)
                .HasColumnType("text")
                .IsRequired();
        });

        modelBuilder.Entity<WishlistItem>(entity =>
        {
            entity.ToTable("WishlistItems");
            entity.HasKey(item => new { item.UserId, item.ProductId });
            entity.Property(item => item.UserId)
                .HasMaxLength(32)
                .IsRequired();
            entity.Property(item => item.ProductId)
                .HasMaxLength(32)
                .IsRequired();
            entity.Property(item => item.CreatedAt)
                .HasColumnType("timestamp with time zone");

            entity.HasOne(item => item.User)
                .WithMany(user => user.WishlistItems)
                .HasForeignKey(item => item.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(item => item.Product)
                .WithMany(product => product.WishlistItems)
                .HasForeignKey(item => item.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Product>().HasData(
            new Product { Id = "p1", Name = "Tailored Italian Suit", Brand = "LUXE", Price = 850m, OriginalPrice = 1100m, Discount = 22, Rating = 4.9m, ReviewsCount = 124, Category = "men", Subcategory = "Suits", Description = "Impeccably tailored from premium Italian wool, this suit offers a sharp, modern silhouette perfect for any formal occasion. Features a fully lined jacket and flat-front trousers.", IsNew = true, IsBestSeller = true, IsTrending = false, IsLimited = false, InStock = true },
            new Product { Id = "p2", Name = "Classic White Oxford", Brand = "MAISON", Price = 125m, OriginalPrice = null, Discount = null, Rating = 4.7m, ReviewsCount = 310, Category = "men", Subcategory = "Shirts", Description = "The quintessential white Oxford shirt. Crisp, breathable cotton and a tailored fit make this a versatile staple for both office and weekend wear.", IsNew = false, IsBestSeller = true, IsTrending = false, IsLimited = false, InStock = true },
            new Product { Id = "p3", Name = "Merino Wool Sweater", Brand = "NORD", Price = 135m, OriginalPrice = 180m, Discount = 25, Rating = 4.8m, ReviewsCount = 278, Category = "men", Subcategory = "Knitwear", Description = "Refined merino wool crew-neck sweater with a classic dropped shoulder. Lightweight yet warm, this versatile piece pairs with everything from denim to tailored trousers.", IsNew = false, IsBestSeller = false, IsTrending = true, IsLimited = false, InStock = true },
            new Product { Id = "p4", Name = "Tailored Slim Trousers", Brand = "MAISON", Price = 145m, OriginalPrice = null, Discount = null, Rating = 4.6m, ReviewsCount = 156, Category = "men", Subcategory = "Trousers", Description = "Sharp, slim-cut trousers crafted from premium Italian wool blend. Features a flat front, side pockets, and a clean finish perfect for both formal and smart-casual occasions.", IsNew = false, IsBestSeller = false, IsTrending = false, IsLimited = false, InStock = true },
            new Product { Id = "p5", Name = "Classic Oxford Shoes", Brand = "MAISON", Price = 285m, OriginalPrice = 375m, Discount = 24, Rating = 4.8m, ReviewsCount = 143, Category = "shoes", Subcategory = "Formal", Description = "Handcrafted Oxford shoes made from smooth calfskin leather with a Goodyear-welted sole. A timeless wardrobe essential that only improves with age.", IsNew = false, IsBestSeller = false, IsTrending = false, IsLimited = true, InStock = true },
            new Product { Id = "p6", Name = "Casual Denim Jacket", Brand = "NORD", Price = 150m, OriginalPrice = null, Discount = null, Rating = 4.5m, ReviewsCount = 320, Category = "men", Subcategory = "Outerwear", Description = "A rugged, classic denim jacket with timeless appeal. Built to last and soften with every wear, featuring button-flap chest pockets and adjustable side tabs.", IsNew = false, IsBestSeller = true, IsTrending = false, IsLimited = false, InStock = true },
            new Product { Id = "p7", Name = "Minimalist Leather Watch", Brand = "LUXE", Price = 250m, OriginalPrice = null, Discount = null, Rating = 4.7m, ReviewsCount = 185, Category = "accessories", Subcategory = "Watches", Description = "A sleek timepiece combining a precise quartz movement with a premium genuine leather strap. Features a minimalist dial that suits both casual and formal attire.", IsNew = true, IsBestSeller = false, IsTrending = false, IsLimited = false, InStock = true },
            new Product { Id = "p8", Name = "Straight Leg Chinos", Brand = "MAISON", Price = 95m, OriginalPrice = 130m, Discount = 27, Rating = 4.5m, ReviewsCount = 267, Category = "men", Subcategory = "Trousers", Description = "Clean and versatile straight-leg chinos in a premium cotton twill. The perfect balance of comfort and refinement for any occasion.", IsNew = false, IsBestSeller = false, IsTrending = true, IsLimited = false, InStock = true }
        );

        modelBuilder.Entity<ProductImage>().HasData(
            new ProductImage { Id = 1, ProductId = "p1", Url = "https://images.unsplash.com/photo-1618886614638-80e3c103d31a?w=800&q=80", SortOrder = 1 },
            new ProductImage { Id = 2, ProductId = "p1", Url = "https://images.unsplash.com/photo-1594938298603-c8148c4dae35?w=800&q=80", SortOrder = 2 },
            new ProductImage { Id = 3, ProductId = "p2", Url = "https://images.unsplash.com/photo-1603252109303-2751441dd157?w=800&q=80", SortOrder = 1 },
            new ProductImage { Id = 4, ProductId = "p2", Url = "https://images.unsplash.com/photo-1596755094514-f87e32f85e2c?w=800&q=80", SortOrder = 2 },
            new ProductImage { Id = 5, ProductId = "p3", Url = "https://images.unsplash.com/photo-1574427797991-b086946fa9e7?w=800&q=80", SortOrder = 1 },
            new ProductImage { Id = 6, ProductId = "p3", Url = "https://images.unsplash.com/photo-1554925051-f668ed70d520?w=800&q=80", SortOrder = 2 },
            new ProductImage { Id = 7, ProductId = "p4", Url = "https://images.unsplash.com/photo-1582225905616-c3adc77ada6b?w=800&q=80", SortOrder = 1 },
            new ProductImage { Id = 8, ProductId = "p4", Url = "https://images.unsplash.com/photo-1523585298601-d46ae038d7d3?w=800&q=80", SortOrder = 2 },
            new ProductImage { Id = 9, ProductId = "p5", Url = "https://images.unsplash.com/photo-1668069226492-508742b03147?w=800&q=80", SortOrder = 1 },
            new ProductImage { Id = 10, ProductId = "p5", Url = "https://images.unsplash.com/photo-1614252339460-e143130d2aa2?w=800&q=80", SortOrder = 2 },
            new ProductImage { Id = 11, ProductId = "p6", Url = "https://images.unsplash.com/photo-1495105787522-5334e3ffa0ebd?w=800&q=80", SortOrder = 1 },
            new ProductImage { Id = 12, ProductId = "p6", Url = "https://images.unsplash.com/photo-1543076447-215ad9ba6923?w=800&q=80", SortOrder = 2 },
            new ProductImage { Id = 13, ProductId = "p7", Url = "https://images.unsplash.com/photo-1542496658-e33a6d0d50f6?w=800&q=80", SortOrder = 1 },
            new ProductImage { Id = 14, ProductId = "p7", Url = "https://images.unsplash.com/photo-1524805444758-089113d48a6d?w=800&q=80", SortOrder = 2 },
            new ProductImage { Id = 15, ProductId = "p8", Url = "https://images.unsplash.com/photo-1617114919297-3c8ddb01f599?w=800&q=80", SortOrder = 1 },
            new ProductImage { Id = 16, ProductId = "p8", Url = "https://images.unsplash.com/photo-1552374196-1ab2a1c593e8?w=800&q=80", SortOrder = 2 }
        );

        modelBuilder.Entity<ProductColor>().HasData(
            new ProductColor { Id = 1, ProductId = "p1", Value = "#1a1a1a" },
            new ProductColor { Id = 2, ProductId = "p1", Value = "#2c3e50" },
            new ProductColor { Id = 3, ProductId = "p2", Value = "#ffffff" },
            new ProductColor { Id = 4, ProductId = "p2", Value = "#f5f5f0" },
            new ProductColor { Id = 5, ProductId = "p3", Value = "#4a4a4a" },
            new ProductColor { Id = 6, ProductId = "p3", Value = "#1a1a1a" },
            new ProductColor { Id = 7, ProductId = "p3", Value = "#d4c5a9" },
            new ProductColor { Id = 8, ProductId = "p3", Value = "#8b4513" },
            new ProductColor { Id = 9, ProductId = "p4", Value = "#1a1a1a" },
            new ProductColor { Id = 10, ProductId = "p4", Value = "#4a4a4a" },
            new ProductColor { Id = 11, ProductId = "p4", Value = "#c4a882" },
            new ProductColor { Id = 12, ProductId = "p4", Value = "#2c3e50" },
            new ProductColor { Id = 13, ProductId = "p5", Value = "#1a1a1a" },
            new ProductColor { Id = 14, ProductId = "p5", Value = "#8b4513" },
            new ProductColor { Id = 15, ProductId = "p5", Value = "#c4a882" },
            new ProductColor { Id = 16, ProductId = "p6", Value = "#2c3e50" },
            new ProductColor { Id = 17, ProductId = "p6", Value = "#1a1a1a" },
            new ProductColor { Id = 18, ProductId = "p6", Value = "#d4c5a9" },
            new ProductColor { Id = 19, ProductId = "p7", Value = "#1a1a1a" },
            new ProductColor { Id = 20, ProductId = "p7", Value = "#8b4513" },
            new ProductColor { Id = 21, ProductId = "p8", Value = "#c4a882" },
            new ProductColor { Id = 22, ProductId = "p8", Value = "#1a1a1a" },
            new ProductColor { Id = 23, ProductId = "p8", Value = "#4a4a4a" },
            new ProductColor { Id = 24, ProductId = "p8", Value = "#2c3e50" }
        );

        modelBuilder.Entity<ProductSize>().HasData(
            new ProductSize { Id = 1, ProductId = "p1", Value = "38R" },
            new ProductSize { Id = 2, ProductId = "p1", Value = "40R" },
            new ProductSize { Id = 3, ProductId = "p1", Value = "42R" },
            new ProductSize { Id = 4, ProductId = "p1", Value = "44R" },
            new ProductSize { Id = 5, ProductId = "p2", Value = "S" },
            new ProductSize { Id = 6, ProductId = "p2", Value = "M" },
            new ProductSize { Id = 7, ProductId = "p2", Value = "L" },
            new ProductSize { Id = 8, ProductId = "p2", Value = "XL" },
            new ProductSize { Id = 9, ProductId = "p2", Value = "XXL" },
            new ProductSize { Id = 10, ProductId = "p3", Value = "S" },
            new ProductSize { Id = 11, ProductId = "p3", Value = "M" },
            new ProductSize { Id = 12, ProductId = "p3", Value = "L" },
            new ProductSize { Id = 13, ProductId = "p3", Value = "XL" },
            new ProductSize { Id = 14, ProductId = "p3", Value = "XXL" },
            new ProductSize { Id = 15, ProductId = "p4", Value = "28" },
            new ProductSize { Id = 16, ProductId = "p4", Value = "30" },
            new ProductSize { Id = 17, ProductId = "p4", Value = "32" },
            new ProductSize { Id = 18, ProductId = "p4", Value = "34" },
            new ProductSize { Id = 19, ProductId = "p4", Value = "36" },
            new ProductSize { Id = 20, ProductId = "p4", Value = "38" },
            new ProductSize { Id = 21, ProductId = "p5", Value = "39" },
            new ProductSize { Id = 22, ProductId = "p5", Value = "40" },
            new ProductSize { Id = 23, ProductId = "p5", Value = "41" },
            new ProductSize { Id = 24, ProductId = "p5", Value = "42" },
            new ProductSize { Id = 25, ProductId = "p5", Value = "43" },
            new ProductSize { Id = 26, ProductId = "p5", Value = "44" },
            new ProductSize { Id = 27, ProductId = "p5", Value = "45" },
            new ProductSize { Id = 28, ProductId = "p6", Value = "S" },
            new ProductSize { Id = 29, ProductId = "p6", Value = "M" },
            new ProductSize { Id = 30, ProductId = "p6", Value = "L" },
            new ProductSize { Id = 31, ProductId = "p6", Value = "XL" },
            new ProductSize { Id = 32, ProductId = "p7", Value = "One Size" },
            new ProductSize { Id = 33, ProductId = "p8", Value = "28" },
            new ProductSize { Id = 34, ProductId = "p8", Value = "30" },
            new ProductSize { Id = 35, ProductId = "p8", Value = "32" },
            new ProductSize { Id = 36, ProductId = "p8", Value = "34" },
            new ProductSize { Id = 37, ProductId = "p8", Value = "36" },
            new ProductSize { Id = 38, ProductId = "p8", Value = "38" },
            new ProductSize { Id = 39, ProductId = "p8", Value = "40" }
        );

        modelBuilder.Entity<ProductTag>().HasData(
            new ProductTag { Id = 1, ProductId = "p1", Value = "suit" },
            new ProductTag { Id = 2, ProductId = "p1", Value = "formal" },
            new ProductTag { Id = 3, ProductId = "p1", Value = "wool" },
            new ProductTag { Id = 4, ProductId = "p1", Value = "italian" },
            new ProductTag { Id = 5, ProductId = "p2", Value = "shirt" },
            new ProductTag { Id = 6, ProductId = "p2", Value = "oxford" },
            new ProductTag { Id = 7, ProductId = "p2", Value = "white" },
            new ProductTag { Id = 8, ProductId = "p2", Value = "staple" },
            new ProductTag { Id = 9, ProductId = "p2", Value = "cotton" },
            new ProductTag { Id = 10, ProductId = "p3", Value = "merino" },
            new ProductTag { Id = 11, ProductId = "p3", Value = "wool" },
            new ProductTag { Id = 12, ProductId = "p3", Value = "sweater" },
            new ProductTag { Id = 13, ProductId = "p3", Value = "knitwear" },
            new ProductTag { Id = 14, ProductId = "p3", Value = "versatile" },
            new ProductTag { Id = 15, ProductId = "p4", Value = "trousers" },
            new ProductTag { Id = 16, ProductId = "p4", Value = "slim" },
            new ProductTag { Id = 17, ProductId = "p4", Value = "tailored" },
            new ProductTag { Id = 18, ProductId = "p4", Value = "formal" },
            new ProductTag { Id = 19, ProductId = "p5", Value = "oxford" },
            new ProductTag { Id = 20, ProductId = "p5", Value = "shoes" },
            new ProductTag { Id = 21, ProductId = "p5", Value = "leather" },
            new ProductTag { Id = 22, ProductId = "p5", Value = "formal" },
            new ProductTag { Id = 23, ProductId = "p5", Value = "italian" },
            new ProductTag { Id = 24, ProductId = "p6", Value = "denim" },
            new ProductTag { Id = 25, ProductId = "p6", Value = "jacket" },
            new ProductTag { Id = 26, ProductId = "p6", Value = "casual" },
            new ProductTag { Id = 27, ProductId = "p6", Value = "outerwear" },
            new ProductTag { Id = 28, ProductId = "p7", Value = "watch" },
            new ProductTag { Id = 29, ProductId = "p7", Value = "leather" },
            new ProductTag { Id = 30, ProductId = "p7", Value = "accessory" },
            new ProductTag { Id = 31, ProductId = "p7", Value = "minimalist" },
            new ProductTag { Id = 32, ProductId = "p8", Value = "chinos" },
            new ProductTag { Id = 33, ProductId = "p8", Value = "casual" },
            new ProductTag { Id = 34, ProductId = "p8", Value = "versatile" },
            new ProductTag { Id = 35, ProductId = "p8", Value = "cotton" }
        );

        modelBuilder.Entity<ProductSpecification>().HasData(
            new ProductSpecification { Id = 1, ProductId = "p1", Name = "Material", Value = "100% Wool" },
            new ProductSpecification { Id = 2, ProductId = "p1", Name = "Care", Value = "Dry Clean Only" },
            new ProductSpecification { Id = 3, ProductId = "p1", Name = "Fit", Value = "Slim Fit" },
            new ProductSpecification { Id = 4, ProductId = "p1", Name = "Origin", Value = "Italy" },
            new ProductSpecification { Id = 5, ProductId = "p2", Name = "Material", Value = "100% Cotton" },
            new ProductSpecification { Id = 6, ProductId = "p2", Name = "Care", Value = "Machine Wash" },
            new ProductSpecification { Id = 7, ProductId = "p2", Name = "Fit", Value = "Tailored" },
            new ProductSpecification { Id = 8, ProductId = "p2", Name = "Collar", Value = "Button-down" },
            new ProductSpecification { Id = 9, ProductId = "p3", Name = "Material", Value = "100% Extra-Fine Merino Wool" },
            new ProductSpecification { Id = 10, ProductId = "p3", Name = "Gauge", Value = "12-Gauge Knit" },
            new ProductSpecification { Id = 11, ProductId = "p3", Name = "Care", Value = "Machine Wash Gentle" },
            new ProductSpecification { Id = 12, ProductId = "p3", Name = "Fit", Value = "Regular" },
            new ProductSpecification { Id = 13, ProductId = "p4", Name = "Material", Value = "70% Wool, 30% Polyester" },
            new ProductSpecification { Id = 14, ProductId = "p4", Name = "Care", Value = "Dry Clean" },
            new ProductSpecification { Id = 15, ProductId = "p4", Name = "Fit", Value = "Slim Straight" },
            new ProductSpecification { Id = 16, ProductId = "p4", Name = "Rise", Value = "Mid Rise" },
            new ProductSpecification { Id = 17, ProductId = "p5", Name = "Material", Value = "Calfskin Leather Upper" },
            new ProductSpecification { Id = 18, ProductId = "p5", Name = "Sole", Value = "Leather Goodyear Welt" },
            new ProductSpecification { Id = 19, ProductId = "p5", Name = "Lining", Value = "Leather" },
            new ProductSpecification { Id = 20, ProductId = "p5", Name = "Origin", Value = "Italy" },
            new ProductSpecification { Id = 21, ProductId = "p6", Name = "Material", Value = "100% Cotton Denim" },
            new ProductSpecification { Id = 22, ProductId = "p6", Name = "Fit", Value = "Regular" },
            new ProductSpecification { Id = 23, ProductId = "p6", Name = "Care", Value = "Machine Wash Cold" },
            new ProductSpecification { Id = 24, ProductId = "p6", Name = "Origin", Value = "Japan" },
            new ProductSpecification { Id = 25, ProductId = "p7", Name = "Material", Value = "Stainless Steel Case, Leather Strap" },
            new ProductSpecification { Id = 26, ProductId = "p7", Name = "Movement", Value = "Quartz" },
            new ProductSpecification { Id = 27, ProductId = "p7", Name = "WaterResistance", Value = "5 ATM" },
            new ProductSpecification { Id = 28, ProductId = "p7", Name = "Face", Value = "40mm" },
            new ProductSpecification { Id = 29, ProductId = "p8", Name = "Material", Value = "98% Cotton, 2% Elastane" },
            new ProductSpecification { Id = 30, ProductId = "p8", Name = "Rise", Value = "Regular" },
            new ProductSpecification { Id = 31, ProductId = "p8", Name = "Fit", Value = "Straight" },
            new ProductSpecification { Id = 32, ProductId = "p8", Name = "Care", Value = "Machine Wash" }
        );

        modelBuilder.Entity<Review>().HasData(
            new Review { Id = 1, ProductId = "p1", User = "James W.", AvatarUrl = "https://images.unsplash.com/photo-1500648767791-00dcc994a43e?w=60&q=80", Rating = 5, Date = new DateOnly(2026, 5, 15), Comment = "The tailoring on this suit is exceptional. It fits perfectly right out of the box and the material feels incredibly premium. Wore it to a wedding and felt great.", Helpful = 24 },
            new Review { Id = 2, ProductId = "p1", User = "Michael T.", AvatarUrl = "https://images.unsplash.com/photo-1472099645785-5658abf4ff4e?w=60&q=80", Rating = 5, Date = new DateOnly(2026, 5, 2), Comment = "Worth the investment. The silhouette is very modern without being too skinny.", Helpful = 18 },
            new Review { Id = 3, ProductId = "p3", User = "David L.", AvatarUrl = "https://images.unsplash.com/photo-1507003211169-0a1dd7228f2d?w=60&q=80", Rating = 4, Date = new DateOnly(2026, 4, 28), Comment = "Great sweater, very soft. I had to size up to get the relaxed fit I wanted, but the quality is top notch.", Helpful = 31 }
        );

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("Users");
            entity.HasKey(u => u.Id);
            entity.HasIndex(u => u.Email).IsUnique();
            entity.Property(u => u.Email).HasMaxLength(150).IsRequired();
            entity.Property(u => u.PasswordHash).HasMaxLength(200).IsRequired();
            entity.Property(u => u.Name).HasMaxLength(100).IsRequired();
            entity.Property(u => u.Role).HasMaxLength(20).IsRequired();
            entity.Property(u => u.Tier).HasMaxLength(20).IsRequired();
        });

        modelBuilder.Entity<User>().HasData(
            new User
            {
                Id = "u1",
                Email = "admin@maeven.com",
                PasswordHash = "$2a$11$cdkcUEXJmsyju9ECU6rAVeuB0XD2ve7OndUrvpgSfXZuqVAYCixUq",
                Name = "Admin User",
                Avatar = "https://images.unsplash.com/photo-1534528741775-53994a69daeb?w=80&q=80",
                Role = "admin",
                Tier = "Platinum",
                CreatedAt = new DateTime(2026, 6, 25, 0, 0, 0, DateTimeKind.Utc)
            },
            new User
            {
                Id = "u2",
                Email = "alex.rivera@email.com",
                PasswordHash = "$2a$11$0RWlqyKf2X.wK01fMRWeDO7xugtg8L4qq7jUhRI4tbcox.7AGFv02",
                Name = "Alexandra Rivera",
                Avatar = "https://images.unsplash.com/photo-1494790108755-2616b332e234?w=80&q=80",
                Role = "user",
                Tier = "Gold",
                CreatedAt = new DateTime(2026, 6, 25, 0, 0, 0, DateTimeKind.Utc)
            }
        );

        modelBuilder.Entity<Order>(entity =>
        {
            entity.ToTable("Orders");
            entity.HasKey(order => order.Id);
            entity.Property(order => order.Id).HasMaxLength(32).IsRequired();
            entity.Property(order => order.UserId).HasMaxLength(32).IsRequired();
            entity.Property(order => order.Status).HasMaxLength(32).IsRequired();
            entity.Property(order => order.PaymentMethod).HasMaxLength(32).IsRequired();
            entity.Property(order => order.PaymentStatus).HasMaxLength(32).IsRequired();
            entity.Property(order => order.DeliveryMethod).HasMaxLength(32).IsRequired();
            entity.Property(order => order.Subtotal).HasPrecision(18, 2);
            entity.Property(order => order.ShippingCost).HasPrecision(18, 2);
            entity.Property(order => order.Tax).HasPrecision(18, 2);
            entity.Property(order => order.Total).HasPrecision(18, 2);
            entity.Property(order => order.CreatedAt).HasColumnType("timestamp with time zone");

            entity.HasOne(order => order.User)
                .WithMany(user => user.Orders)
                .HasForeignKey(order => order.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasMany(order => order.Items)
                .WithOne(item => item.Order)
                .HasForeignKey(item => item.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(order => order.ShippingAddress)
                .WithOne(address => address.Order)
                .HasForeignKey<ShippingAddress>(address => address.OrderId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.ToTable("OrderItems");
            entity.HasKey(item => item.Id);
            entity.Property(item => item.OrderId).HasMaxLength(32).IsRequired();
            entity.Property(item => item.ProductId).HasMaxLength(32).IsRequired();
            entity.Property(item => item.ProductName).HasMaxLength(200).IsRequired();
            entity.Property(item => item.ProductImage).HasColumnType("text").IsRequired();
            entity.Property(item => item.UnitPrice).HasPrecision(18, 2);
            entity.Property(item => item.Size).HasMaxLength(32).IsRequired();
            entity.Property(item => item.Color).HasMaxLength(32).IsRequired();

            entity.HasOne(item => item.Product)
                .WithMany()
                .HasForeignKey(item => item.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<ShippingAddress>(entity =>
        {
            entity.ToTable("ShippingAddresses");
            entity.HasKey(address => address.Id);
            entity.HasIndex(address => address.OrderId).IsUnique();
            entity.Property(address => address.OrderId).HasMaxLength(32).IsRequired();
            entity.Property(address => address.FirstName).HasMaxLength(100).IsRequired();
            entity.Property(address => address.LastName).HasMaxLength(100).IsRequired();
            entity.Property(address => address.Email).HasMaxLength(150).IsRequired();
            entity.Property(address => address.Phone).HasMaxLength(32).IsRequired();
            entity.Property(address => address.Address).HasColumnType("text").IsRequired();
            entity.Property(address => address.Country).HasMaxLength(80).IsRequired();
        });
    }
}
