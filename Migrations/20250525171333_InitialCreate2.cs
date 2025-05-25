using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace celebrations.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9ed08212-d68c-4cc5-b7b9-a7d9d27497d1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b2f12e99-e4f9-48b1-b5a4-57334f82ca28");

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<string>(type: "text", nullable: false),
                    ProductName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Slug = table.Column<string>(type: "text", nullable: false),
                    ProductImage = table.Column<string>(type: "text", nullable: true),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    DiscountStart = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DiscountEnd = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "079c069e-0fdb-4cdb-9d75-08d3c86bbbf7", "1", "Admin", "ADMIN" },
                    { "898d5668-412c-4263-b3bd-666d7bc6cbaa", "2", "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "DiscountEnd", "DiscountStart", "Price", "ProductImage", "ProductName", "Slug" },
                values: new object[,]
                {
                    { "6e2880d8-826c-4e41-8ccf-20e850f50698", new DateTime(2025, 6, 1, 17, 13, 33, 125, DateTimeKind.Utc).AddTicks(9878), new DateTime(2025, 5, 25, 17, 13, 33, 125, DateTimeKind.Utc).AddTicks(9875), 25.99m, "mouse.jpg", "Wireless Mouse", "wireless-mouse" },
                    { "b8035b58-85ef-42ca-bdaa-f52bd7099041", new DateTime(2025, 6, 4, 17, 13, 33, 125, DateTimeKind.Utc).AddTicks(9892), new DateTime(2025, 5, 25, 17, 13, 33, 125, DateTimeKind.Utc).AddTicks(9891), 199.99m, "monitor.jpg", "HD Monitor", "hd-monitor" },
                    { "e03e0c53-8b68-4274-a3cc-f6dd469e87ce", new DateTime(2025, 6, 1, 17, 13, 33, 125, DateTimeKind.Utc).AddTicks(9888), new DateTime(2025, 5, 25, 17, 13, 33, 125, DateTimeKind.Utc).AddTicks(9888), 79.99m, "keyboard.jpg", "Mechanical Keyboard", "mechanical-keyboard" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductName",
                table: "Products",
                column: "ProductName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "079c069e-0fdb-4cdb-9d75-08d3c86bbbf7");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "898d5668-412c-4263-b3bd-666d7bc6cbaa");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "9ed08212-d68c-4cc5-b7b9-a7d9d27497d1", "2", "User", "USER" },
                    { "b2f12e99-e4f9-48b1-b5a4-57334f82ca28", "1", "Admin", "ADMIN" }
                });
        }
    }
}
