using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace celebrations.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "079c069e-0fdb-4cdb-9d75-08d3c86bbbf7");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "898d5668-412c-4263-b3bd-666d7bc6cbaa");

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: "6e2880d8-826c-4e41-8ccf-20e850f50698");

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: "b8035b58-85ef-42ca-bdaa-f52bd7099041");

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: "e03e0c53-8b68-4274-a3cc-f6dd469e87ce");

            migrationBuilder.CreateTable(
                name: "Carts",
                columns: table => new
                {
                    CartId = table.Column<string>(type: "text", nullable: false),
                    ProductId = table.Column<string>(type: "text", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carts", x => x.CartId);
                    table.ForeignKey(
                        name: "FK_Carts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1d94964c-2cbe-4661-beeb-818de4ff21bc", "2", "User", "USER" },
                    { "c4ad2539-2c15-4a88-acf0-74c1f9415b77", "1", "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "DiscountEnd", "DiscountStart", "Price", "ProductImage", "ProductName", "Slug" },
                values: new object[,]
                {
                    { "2dbcf6af-75a7-41a3-bb19-8c32ae021c12", new DateTime(2025, 6, 1, 18, 45, 3, 640, DateTimeKind.Utc).AddTicks(1045), new DateTime(2025, 5, 25, 18, 45, 3, 640, DateTimeKind.Utc).AddTicks(1039), 25.99m, "mouse.jpg", "Wireless Mouse", "wireless-mouse" },
                    { "30a973f7-5dd7-45c1-9049-873417f15376", new DateTime(2025, 6, 4, 18, 45, 3, 640, DateTimeKind.Utc).AddTicks(1078), new DateTime(2025, 5, 25, 18, 45, 3, 640, DateTimeKind.Utc).AddTicks(1076), 199.99m, "monitor.jpg", "HD Monitor", "hd-monitor" },
                    { "f5e99b0d-a4d9-425e-945e-a345fa1505df", new DateTime(2025, 6, 1, 18, 45, 3, 640, DateTimeKind.Utc).AddTicks(1067), new DateTime(2025, 5, 25, 18, 45, 3, 640, DateTimeKind.Utc).AddTicks(1066), 79.99m, "keyboard.jpg", "Mechanical Keyboard", "mechanical-keyboard" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Carts_ProductId",
                table: "Carts",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Carts");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1d94964c-2cbe-4661-beeb-818de4ff21bc");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c4ad2539-2c15-4a88-acf0-74c1f9415b77");

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: "2dbcf6af-75a7-41a3-bb19-8c32ae021c12");

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: "30a973f7-5dd7-45c1-9049-873417f15376");

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: "f5e99b0d-a4d9-425e-945e-a345fa1505df");

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
        }
    }
}
