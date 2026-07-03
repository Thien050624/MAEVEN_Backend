using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MAEVEN.Backend.Migrations
{
    /// <inheritdoc />
    public partial class MakeWishlistUserScoped : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_WishlistItems",
                table: "WishlistItems");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "WishlistItems",
                type: "character varying(32)",
                maxLength: 32,
                nullable: false,
                defaultValue: "");

            migrationBuilder.Sql("""
                UPDATE "WishlistItems"
                SET "UserId" = COALESCE(
                    (SELECT "Id" FROM "Users" ORDER BY "CreatedAt" LIMIT 1),
                    'u2'
                )
                WHERE "UserId" = '';
                """);

            migrationBuilder.AddPrimaryKey(
                name: "PK_WishlistItems",
                table: "WishlistItems",
                columns: new[] { "UserId", "ProductId" });

            migrationBuilder.CreateIndex(
                name: "IX_WishlistItems_ProductId",
                table: "WishlistItems",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_WishlistItems_Users_UserId",
                table: "WishlistItems",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WishlistItems_Users_UserId",
                table: "WishlistItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WishlistItems",
                table: "WishlistItems");

            migrationBuilder.DropIndex(
                name: "IX_WishlistItems_ProductId",
                table: "WishlistItems");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "WishlistItems");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WishlistItems",
                table: "WishlistItems",
                column: "ProductId");
        }
    }
}
