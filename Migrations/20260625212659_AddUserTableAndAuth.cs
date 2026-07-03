using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MAEVEN.Backend.Migrations
{
    /// <inheritdoc />
    public partial class AddUserTableAndAuth : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    PasswordHash = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Avatar = table.Column<string>(type: "text", nullable: false),
                    Role = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Tier = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Avatar", "CreatedAt", "Email", "Name", "PasswordHash", "Role", "Tier" },
                values: new object[,]
                {
                    { "u1", "https://images.unsplash.com/photo-1534528741775-53994a69daeb?w=80&q=80", new DateTime(2026, 6, 25, 0, 0, 0, 0, DateTimeKind.Utc), "admin@maeven.com", "Admin User", "$2a$11$cdkcUEXJmsyju9ECU6rAVeuB0XD2ve7OndUrvpgSfXZuqVAYCixUq", "admin", "Platinum" },
                    { "u2", "https://images.unsplash.com/photo-1494790108755-2616b332e234?w=80&q=80", new DateTime(2026, 6, 25, 0, 0, 0, 0, DateTimeKind.Utc), "alex.rivera@email.com", "Alexandra Rivera", "$2a$11$0RWlqyKf2X.wK01fMRWeDO7xugtg8L4qq7jUhRI4tbcox.7AGFv02", "user", "Gold" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
