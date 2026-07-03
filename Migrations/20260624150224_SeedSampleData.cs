using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MAEVEN.Backend.Migrations
{
    /// <inheritdoc />
    public partial class SeedSampleData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Brand", "Category", "Description", "Discount", "InStock", "IsBestSeller", "IsLimited", "IsNew", "IsTrending", "Name", "OriginalPrice", "Price", "Rating", "ReviewsCount", "Subcategory" },
                values: new object[,]
                {
                    { "p1", "LUXE", "men", "Impeccably tailored from premium Italian wool, this suit offers a sharp, modern silhouette perfect for any formal occasion. Features a fully lined jacket and flat-front trousers.", 22, true, true, false, true, false, "Tailored Italian Suit", 1100m, 850m, 4.9m, 124, "Suits" },
                    { "p2", "MAISON", "men", "The quintessential white Oxford shirt. Crisp, breathable cotton and a tailored fit make this a versatile staple for both office and weekend wear.", null, true, true, false, false, false, "Classic White Oxford", null, 125m, 4.7m, 310, "Shirts" },
                    { "p3", "NORD", "men", "Refined merino wool crew-neck sweater with a classic dropped shoulder. Lightweight yet warm, this versatile piece pairs with everything from denim to tailored trousers.", 25, true, false, false, false, true, "Merino Wool Sweater", 180m, 135m, 4.8m, 278, "Knitwear" },
                    { "p4", "MAISON", "men", "Sharp, slim-cut trousers crafted from premium Italian wool blend. Features a flat front, side pockets, and a clean finish perfect for both formal and smart-casual occasions.", null, true, false, false, false, false, "Tailored Slim Trousers", null, 145m, 4.6m, 156, "Trousers" },
                    { "p5", "MAISON", "shoes", "Handcrafted Oxford shoes made from smooth calfskin leather with a Goodyear-welted sole. A timeless wardrobe essential that only improves with age.", 24, true, false, true, false, false, "Classic Oxford Shoes", 375m, 285m, 4.8m, 143, "Formal" },
                    { "p6", "NORD", "men", "A rugged, classic denim jacket with timeless appeal. Built to last and soften with every wear, featuring button-flap chest pockets and adjustable side tabs.", null, true, true, false, false, false, "Casual Denim Jacket", null, 150m, 4.5m, 320, "Outerwear" },
                    { "p7", "LUXE", "accessories", "A sleek timepiece combining a precise quartz movement with a premium genuine leather strap. Features a minimalist dial that suits both casual and formal attire.", null, true, false, false, true, false, "Minimalist Leather Watch", null, 250m, 4.7m, 185, "Watches" },
                    { "p8", "MAISON", "men", "Clean and versatile straight-leg chinos in a premium cotton twill. The perfect balance of comfort and refinement for any occasion.", 27, true, false, false, false, true, "Straight Leg Chinos", 130m, 95m, 4.5m, 267, "Trousers" }
                });

            migrationBuilder.InsertData(
                table: "ProductColors",
                columns: new[] { "Id", "ProductId", "Value" },
                values: new object[,]
                {
                    { 1, "p1", "#1a1a1a" },
                    { 2, "p1", "#2c3e50" },
                    { 3, "p2", "#ffffff" },
                    { 4, "p2", "#f5f5f0" },
                    { 5, "p3", "#4a4a4a" },
                    { 6, "p3", "#1a1a1a" },
                    { 7, "p3", "#d4c5a9" },
                    { 8, "p3", "#8b4513" },
                    { 9, "p4", "#1a1a1a" },
                    { 10, "p4", "#4a4a4a" },
                    { 11, "p4", "#c4a882" },
                    { 12, "p4", "#2c3e50" },
                    { 13, "p5", "#1a1a1a" },
                    { 14, "p5", "#8b4513" },
                    { 15, "p5", "#c4a882" },
                    { 16, "p6", "#2c3e50" },
                    { 17, "p6", "#1a1a1a" },
                    { 18, "p6", "#d4c5a9" },
                    { 19, "p7", "#1a1a1a" },
                    { 20, "p7", "#8b4513" },
                    { 21, "p8", "#c4a882" },
                    { 22, "p8", "#1a1a1a" },
                    { 23, "p8", "#4a4a4a" },
                    { 24, "p8", "#2c3e50" }
                });

            migrationBuilder.InsertData(
                table: "ProductImages",
                columns: new[] { "Id", "ProductId", "SortOrder", "Url" },
                values: new object[,]
                {
                    { 1, "p1", 1, "https://images.unsplash.com/photo-1618886614638-80e3c103d31a?w=800&q=80" },
                    { 2, "p1", 2, "https://images.unsplash.com/photo-1594938298603-c8148c4dae35?w=800&q=80" },
                    { 3, "p2", 1, "https://images.unsplash.com/photo-1603252109303-2751441dd157?w=800&q=80" },
                    { 4, "p2", 2, "https://images.unsplash.com/photo-1596755094514-f87e32f85e2c?w=800&q=80" },
                    { 5, "p3", 1, "https://images.unsplash.com/photo-1574427797991-b086946fa9e7?w=800&q=80" },
                    { 6, "p3", 2, "https://images.unsplash.com/photo-1554925051-f668ed70d520?w=800&q=80" },
                    { 7, "p4", 1, "https://images.unsplash.com/photo-1582225905616-c3adc77ada6b?w=800&q=80" },
                    { 8, "p4", 2, "https://images.unsplash.com/photo-1523585298601-d46ae038d7d3?w=800&q=80" },
                    { 9, "p5", 1, "https://images.unsplash.com/photo-1668069226492-508742b03147?w=800&q=80" },
                    { 10, "p5", 2, "https://images.unsplash.com/photo-1614252339460-e143130d2aa2?w=800&q=80" },
                    { 11, "p6", 1, "https://images.unsplash.com/photo-1495105787522-5334e3ffa0ebd?w=800&q=80" },
                    { 12, "p6", 2, "https://images.unsplash.com/photo-1543076447-215ad9ba6923?w=800&q=80" },
                    { 13, "p7", 1, "https://images.unsplash.com/photo-1542496658-e33a6d0d50f6?w=800&q=80" },
                    { 14, "p7", 2, "https://images.unsplash.com/photo-1524805444758-089113d48a6d?w=800&q=80" },
                    { 15, "p8", 1, "https://images.unsplash.com/photo-1617114919297-3c8ddb01f599?w=800&q=80" },
                    { 16, "p8", 2, "https://images.unsplash.com/photo-1552374196-1ab2a1c593e8?w=800&q=80" }
                });

            migrationBuilder.InsertData(
                table: "ProductSizes",
                columns: new[] { "Id", "ProductId", "Value" },
                values: new object[,]
                {
                    { 1, "p1", "38R" },
                    { 2, "p1", "40R" },
                    { 3, "p1", "42R" },
                    { 4, "p1", "44R" },
                    { 5, "p2", "S" },
                    { 6, "p2", "M" },
                    { 7, "p2", "L" },
                    { 8, "p2", "XL" },
                    { 9, "p2", "XXL" },
                    { 10, "p3", "S" },
                    { 11, "p3", "M" },
                    { 12, "p3", "L" },
                    { 13, "p3", "XL" },
                    { 14, "p3", "XXL" },
                    { 15, "p4", "28" },
                    { 16, "p4", "30" },
                    { 17, "p4", "32" },
                    { 18, "p4", "34" },
                    { 19, "p4", "36" },
                    { 20, "p4", "38" },
                    { 21, "p5", "39" },
                    { 22, "p5", "40" },
                    { 23, "p5", "41" },
                    { 24, "p5", "42" },
                    { 25, "p5", "43" },
                    { 26, "p5", "44" },
                    { 27, "p5", "45" },
                    { 28, "p6", "S" },
                    { 29, "p6", "M" },
                    { 30, "p6", "L" },
                    { 31, "p6", "XL" },
                    { 32, "p7", "One Size" },
                    { 33, "p8", "28" },
                    { 34, "p8", "30" },
                    { 35, "p8", "32" },
                    { 36, "p8", "34" },
                    { 37, "p8", "36" },
                    { 38, "p8", "38" },
                    { 39, "p8", "40" }
                });

            migrationBuilder.InsertData(
                table: "ProductSpecifications",
                columns: new[] { "Id", "Name", "ProductId", "Value" },
                values: new object[,]
                {
                    { 1, "Material", "p1", "100% Wool" },
                    { 2, "Care", "p1", "Dry Clean Only" },
                    { 3, "Fit", "p1", "Slim Fit" },
                    { 4, "Origin", "p1", "Italy" },
                    { 5, "Material", "p2", "100% Cotton" },
                    { 6, "Care", "p2", "Machine Wash" },
                    { 7, "Fit", "p2", "Tailored" },
                    { 8, "Collar", "p2", "Button-down" },
                    { 9, "Material", "p3", "100% Extra-Fine Merino Wool" },
                    { 10, "Gauge", "p3", "12-Gauge Knit" },
                    { 11, "Care", "p3", "Machine Wash Gentle" },
                    { 12, "Fit", "p3", "Regular" },
                    { 13, "Material", "p4", "70% Wool, 30% Polyester" },
                    { 14, "Care", "p4", "Dry Clean" },
                    { 15, "Fit", "p4", "Slim Straight" },
                    { 16, "Rise", "p4", "Mid Rise" },
                    { 17, "Material", "p5", "Calfskin Leather Upper" },
                    { 18, "Sole", "p5", "Leather Goodyear Welt" },
                    { 19, "Lining", "p5", "Leather" },
                    { 20, "Origin", "p5", "Italy" },
                    { 21, "Material", "p6", "100% Cotton Denim" },
                    { 22, "Fit", "p6", "Regular" },
                    { 23, "Care", "p6", "Machine Wash Cold" },
                    { 24, "Origin", "p6", "Japan" },
                    { 25, "Material", "p7", "Stainless Steel Case, Leather Strap" },
                    { 26, "Movement", "p7", "Quartz" },
                    { 27, "WaterResistance", "p7", "5 ATM" },
                    { 28, "Face", "p7", "40mm" },
                    { 29, "Material", "p8", "98% Cotton, 2% Elastane" },
                    { 30, "Rise", "p8", "Regular" },
                    { 31, "Fit", "p8", "Straight" },
                    { 32, "Care", "p8", "Machine Wash" }
                });

            migrationBuilder.InsertData(
                table: "ProductTags",
                columns: new[] { "Id", "ProductId", "Value" },
                values: new object[,]
                {
                    { 1, "p1", "suit" },
                    { 2, "p1", "formal" },
                    { 3, "p1", "wool" },
                    { 4, "p1", "italian" },
                    { 5, "p2", "shirt" },
                    { 6, "p2", "oxford" },
                    { 7, "p2", "white" },
                    { 8, "p2", "staple" },
                    { 9, "p2", "cotton" },
                    { 10, "p3", "merino" },
                    { 11, "p3", "wool" },
                    { 12, "p3", "sweater" },
                    { 13, "p3", "knitwear" },
                    { 14, "p3", "versatile" },
                    { 15, "p4", "trousers" },
                    { 16, "p4", "slim" },
                    { 17, "p4", "tailored" },
                    { 18, "p4", "formal" },
                    { 19, "p5", "oxford" },
                    { 20, "p5", "shoes" },
                    { 21, "p5", "leather" },
                    { 22, "p5", "formal" },
                    { 23, "p5", "italian" },
                    { 24, "p6", "denim" },
                    { 25, "p6", "jacket" },
                    { 26, "p6", "casual" },
                    { 27, "p6", "outerwear" },
                    { 28, "p7", "watch" },
                    { 29, "p7", "leather" },
                    { 30, "p7", "accessory" },
                    { 31, "p7", "minimalist" },
                    { 32, "p8", "chinos" },
                    { 33, "p8", "casual" },
                    { 34, "p8", "versatile" },
                    { 35, "p8", "cotton" }
                });

            migrationBuilder.InsertData(
                table: "Reviews",
                columns: new[] { "Id", "AvatarUrl", "Comment", "Date", "Helpful", "ProductId", "Rating", "User" },
                values: new object[,]
                {
                    { 1, "https://images.unsplash.com/photo-1500648767791-00dcc994a43e?w=60&q=80", "The tailoring on this suit is exceptional. It fits perfectly right out of the box and the material feels incredibly premium. Wore it to a wedding and felt great.", new DateOnly(2026, 5, 15), 24, "p1", 5, "James W." },
                    { 2, "https://images.unsplash.com/photo-1472099645785-5658abf4ff4e?w=60&q=80", "Worth the investment. The silhouette is very modern without being too skinny.", new DateOnly(2026, 5, 2), 18, "p1", 5, "Michael T." },
                    { 3, "https://images.unsplash.com/photo-1507003211169-0a1dd7228f2d?w=60&q=80", "Great sweater, very soft. I had to size up to get the relaxed fit I wanted, but the quality is top notch.", new DateOnly(2026, 4, 28), 31, "p3", 4, "David L." }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProductColors",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ProductColors",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ProductColors",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ProductColors",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "ProductColors",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "ProductColors",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "ProductColors",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "ProductColors",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "ProductColors",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "ProductColors",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "ProductColors",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "ProductColors",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "ProductColors",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "ProductColors",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "ProductColors",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "ProductColors",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "ProductColors",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "ProductColors",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "ProductColors",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "ProductColors",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "ProductColors",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "ProductColors",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "ProductColors",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "ProductColors",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "ProductSizes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ProductSizes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ProductSizes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ProductSizes",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "ProductSizes",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "ProductSizes",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "ProductSizes",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "ProductSizes",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "ProductSizes",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "ProductSizes",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "ProductSizes",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "ProductSizes",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "ProductSizes",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "ProductSizes",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "ProductSizes",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "ProductSizes",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "ProductSizes",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "ProductSizes",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "ProductSizes",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "ProductSizes",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "ProductSizes",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "ProductSizes",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "ProductSizes",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "ProductSizes",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "ProductSizes",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "ProductSizes",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "ProductSizes",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "ProductSizes",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "ProductSizes",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "ProductSizes",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "ProductSizes",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "ProductSizes",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "ProductSizes",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "ProductSizes",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "ProductSizes",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "ProductSizes",
                keyColumn: "Id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "ProductSizes",
                keyColumn: "Id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "ProductSizes",
                keyColumn: "Id",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "ProductSizes",
                keyColumn: "Id",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "ProductSpecifications",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ProductSpecifications",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ProductSpecifications",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ProductSpecifications",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "ProductSpecifications",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "ProductSpecifications",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "ProductSpecifications",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "ProductSpecifications",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "ProductSpecifications",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "ProductSpecifications",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "ProductSpecifications",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "ProductSpecifications",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "ProductSpecifications",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "ProductSpecifications",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "ProductSpecifications",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "ProductSpecifications",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "ProductSpecifications",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "ProductSpecifications",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "ProductSpecifications",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "ProductSpecifications",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "ProductSpecifications",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "ProductSpecifications",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "ProductSpecifications",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "ProductSpecifications",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "ProductSpecifications",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "ProductSpecifications",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "ProductSpecifications",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "ProductSpecifications",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "ProductSpecifications",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "ProductSpecifications",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "ProductSpecifications",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "ProductSpecifications",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "ProductTags",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ProductTags",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ProductTags",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ProductTags",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "ProductTags",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "ProductTags",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "ProductTags",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "ProductTags",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "ProductTags",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "ProductTags",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "ProductTags",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "ProductTags",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "ProductTags",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "ProductTags",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "ProductTags",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "ProductTags",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "ProductTags",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "ProductTags",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "ProductTags",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "ProductTags",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "ProductTags",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "ProductTags",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "ProductTags",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "ProductTags",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "ProductTags",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "ProductTags",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "ProductTags",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "ProductTags",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "ProductTags",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "ProductTags",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "ProductTags",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "ProductTags",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "ProductTags",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "ProductTags",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "ProductTags",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: "p1");

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: "p2");

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: "p3");

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: "p4");

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: "p5");

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: "p6");

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: "p7");

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: "p8");
        }
    }
}
