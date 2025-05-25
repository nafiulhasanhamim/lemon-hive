using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace celebrations.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "72772b45-d3a0-4cc7-93d9-be55552a20c0");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ee6bdf9f-23ed-4c88-b4dd-07dbb0e3aee4");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "9ed08212-d68c-4cc5-b7b9-a7d9d27497d1", "2", "User", "USER" },
                    { "b2f12e99-e4f9-48b1-b5a4-57334f82ca28", "1", "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9ed08212-d68c-4cc5-b7b9-a7d9d27497d1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b2f12e99-e4f9-48b1-b5a4-57334f82ca28");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "72772b45-d3a0-4cc7-93d9-be55552a20c0", "2", "User", "USER" },
                    { "ee6bdf9f-23ed-4c88-b4dd-07dbb0e3aee4", "1", "Admin", "ADMIN" }
                });
        }
    }
}
