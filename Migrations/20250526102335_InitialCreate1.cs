using System;
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
                keyValue: "510699c3-e0b4-442c-b211-31d3d16a8181");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b35f05b7-fb24-44e7-9a04-82271b0fb33d");

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: "38756ba7-915b-478b-90ee-fa134c65173b");

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: "46327481-8470-4f66-9a71-7eaad80ce43f");

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: "5c1bd019-e868-48d6-9bcb-2869e061eaae");

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: "6f5d1fe9-b965-45f5-90e0-bdcf724e0c73");

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: "b93abafe-7fd2-48d1-a79c-e01b195b7fdb");

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: "d2897e3b-a5de-4d45-ab34-29251bf7f7ee");

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: "d7da0a39-c6f9-47be-849f-875e69a0dd2c");

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: "ed09b0b5-fd87-4b56-bbff-158936032289");

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: "f5a0fd9a-35f3-4b57-b5ff-e230eb7a301d");

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: "fa06c4c9-a3e5-4903-959b-c5f9ae97c452");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "71995708-4c32-4300-b85a-75326a4a9ea0", "2", "User", "USER" },
                    { "a0b2fd5f-1f2b-45cc-b046-3f0664b9c7ac", "1", "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "DiscountEnd", "DiscountStart", "Price", "ProductImage", "ProductName", "Slug" },
                values: new object[,]
                {
                    { "0b4bf650-cd4a-4579-937d-e15931b32c03", new DateTime(2025, 6, 6, 10, 23, 34, 151, DateTimeKind.Utc).AddTicks(4948), new DateTime(2025, 6, 5, 10, 23, 34, 151, DateTimeKind.Utc).AddTicks(4947), 129.99m, "ssd.jpg", "External SSD 1TB", "external-ssd-1tb" },
                    { "2d9bf373-6aab-4325-b317-d2ddeee9a21e", new DateTime(2025, 6, 2, 10, 23, 34, 151, DateTimeKind.Utc).AddTicks(4902), new DateTime(2025, 5, 26, 10, 23, 34, 151, DateTimeKind.Utc).AddTicks(4901), 79.99m, "keyboard.jpg", "Mechanical Keyboard", "mechanical-keyboard" },
                    { "2e04abcb-6940-4869-bc2b-8403180f1b1d", new DateTime(2025, 5, 31, 10, 23, 34, 151, DateTimeKind.Utc).AddTicks(4954), new DateTime(2025, 5, 27, 10, 23, 34, 151, DateTimeKind.Utc).AddTicks(4953), 159.99m, "headphones.jpg", "Noise-Cancelling Headphones", "nc-headphones" },
                    { "559fdf13-02ff-4079-bfa0-5f79a15461f8", new DateTime(2025, 6, 5, 10, 23, 34, 151, DateTimeKind.Utc).AddTicks(4916), new DateTime(2025, 5, 27, 10, 23, 34, 151, DateTimeKind.Utc).AddTicks(4915), 139.50m, "chair.jpg", "Gaming Chair", "gaming-chair" },
                    { "6e01ed92-f236-4bdf-919b-0c1a767495a1", new DateTime(2025, 6, 9, 10, 23, 34, 151, DateTimeKind.Utc).AddTicks(4928), new DateTime(2025, 5, 26, 10, 23, 34, 151, DateTimeKind.Utc).AddTicks(4927), 59.99m, "webcam.jpg", "1080p Webcam Pro", "webcam-pro-1080p" },
                    { "a0e124e7-2d33-48e5-9038-63466f0eba8b", new DateTime(2025, 5, 31, 10, 23, 34, 151, DateTimeKind.Utc).AddTicks(4909), new DateTime(2025, 5, 28, 10, 23, 34, 151, DateTimeKind.Utc).AddTicks(4908), 199.99m, "monitor27.jpg", "HD Monitor 27\"", "hd-monitor-27" },
                    { "b2d19f24-0f23-43ea-bc59-01de84774bff", new DateTime(2025, 6, 25, 10, 23, 34, 151, DateTimeKind.Utc).AddTicks(4961), new DateTime(2025, 5, 26, 10, 23, 34, 151, DateTimeKind.Utc).AddTicks(4960), 799.00m, "tv.jpg", "4K Ultra HD TV", "4k-ultra-hd-tv" },
                    { "b93fcc2f-4dd4-49c6-91e7-6d2fb022d3f8", new DateTime(2025, 5, 30, 10, 23, 34, 151, DateTimeKind.Utc).AddTicks(4922), new DateTime(2025, 5, 29, 10, 23, 34, 151, DateTimeKind.Utc).AddTicks(4921), 45.00m, "hub.jpg", "USB-C Hub (6-in-1)", "usb-c-hub-6-in-1" },
                    { "f4aef094-93b8-429f-8408-5356aec396bc", new DateTime(2025, 6, 1, 10, 23, 34, 151, DateTimeKind.Utc).AddTicks(4880), new DateTime(2025, 5, 27, 10, 23, 34, 151, DateTimeKind.Utc).AddTicks(4869), 25.99m, "mouse.jpg", "Wireless Mouse", "wireless-mouse" },
                    { "fecd4431-542f-4bc4-8dfd-5c132f706ab1", new DateTime(2025, 6, 1, 10, 23, 34, 151, DateTimeKind.Utc).AddTicks(4934), new DateTime(2025, 5, 31, 10, 23, 34, 151, DateTimeKind.Utc).AddTicks(4933), 89.95m, "speaker.jpg", "Bluetooth Speaker", "bluetooth-speaker" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "71995708-4c32-4300-b85a-75326a4a9ea0");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a0b2fd5f-1f2b-45cc-b046-3f0664b9c7ac");

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: "0b4bf650-cd4a-4579-937d-e15931b32c03");

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: "2d9bf373-6aab-4325-b317-d2ddeee9a21e");

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: "2e04abcb-6940-4869-bc2b-8403180f1b1d");

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: "559fdf13-02ff-4079-bfa0-5f79a15461f8");

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: "6e01ed92-f236-4bdf-919b-0c1a767495a1");

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: "a0e124e7-2d33-48e5-9038-63466f0eba8b");

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: "b2d19f24-0f23-43ea-bc59-01de84774bff");

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: "b93fcc2f-4dd4-49c6-91e7-6d2fb022d3f8");

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: "f4aef094-93b8-429f-8408-5356aec396bc");

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: "fecd4431-542f-4bc4-8dfd-5c132f706ab1");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "510699c3-e0b4-442c-b211-31d3d16a8181", "1", "Admin", "ADMIN" },
                    { "b35f05b7-fb24-44e7-9a04-82271b0fb33d", "2", "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "DiscountEnd", "DiscountStart", "Price", "ProductImage", "ProductName", "Slug" },
                values: new object[,]
                {
                    { "38756ba7-915b-478b-90ee-fa134c65173b", new DateTime(2025, 5, 31, 8, 15, 8, 938, DateTimeKind.Utc).AddTicks(4184), new DateTime(2025, 5, 28, 8, 15, 8, 938, DateTimeKind.Utc).AddTicks(4183), 199.99m, "monitor27.jpg", "HD Monitor 27\"", "hd-monitor-27" },
                    { "46327481-8470-4f66-9a71-7eaad80ce43f", new DateTime(2025, 6, 2, 8, 15, 8, 938, DateTimeKind.Utc).AddTicks(4180), new DateTime(2025, 5, 26, 8, 15, 8, 938, DateTimeKind.Utc).AddTicks(4180), 79.99m, "keyboard.jpg", "Mechanical Keyboard", "mechanical-keyboard" },
                    { "5c1bd019-e868-48d6-9bcb-2869e061eaae", new DateTime(2025, 6, 5, 8, 15, 8, 938, DateTimeKind.Utc).AddTicks(4203), new DateTime(2025, 5, 27, 8, 15, 8, 938, DateTimeKind.Utc).AddTicks(4203), 139.50m, "chair.jpg", "Gaming Chair", "gaming-chair" },
                    { "6f5d1fe9-b965-45f5-90e0-bdcf724e0c73", new DateTime(2025, 6, 6, 8, 15, 8, 938, DateTimeKind.Utc).AddTicks(4257), new DateTime(2025, 6, 5, 8, 15, 8, 938, DateTimeKind.Utc).AddTicks(4257), 129.99m, "ssd.jpg", "External SSD 1TB", "external-ssd-1tb" },
                    { "b93abafe-7fd2-48d1-a79c-e01b195b7fdb", new DateTime(2025, 6, 25, 8, 15, 8, 938, DateTimeKind.Utc).AddTicks(4264), new DateTime(2025, 5, 26, 8, 15, 8, 938, DateTimeKind.Utc).AddTicks(4264), 799.00m, "tv.jpg", "4K Ultra HD TV", "4k-ultra-hd-tv" },
                    { "d2897e3b-a5de-4d45-ab34-29251bf7f7ee", new DateTime(2025, 6, 1, 8, 15, 8, 938, DateTimeKind.Utc).AddTicks(4254), new DateTime(2025, 5, 31, 8, 15, 8, 938, DateTimeKind.Utc).AddTicks(4253), 89.95m, "speaker.jpg", "Bluetooth Speaker", "bluetooth-speaker" },
                    { "d7da0a39-c6f9-47be-849f-875e69a0dd2c", new DateTime(2025, 6, 1, 8, 15, 8, 938, DateTimeKind.Utc).AddTicks(4168), new DateTime(2025, 5, 27, 8, 15, 8, 938, DateTimeKind.Utc).AddTicks(4159), 25.99m, "mouse.jpg", "Wireless Mouse", "wireless-mouse" },
                    { "ed09b0b5-fd87-4b56-bbff-158936032289", new DateTime(2025, 5, 30, 8, 15, 8, 938, DateTimeKind.Utc).AddTicks(4206), new DateTime(2025, 5, 29, 8, 15, 8, 938, DateTimeKind.Utc).AddTicks(4206), 45.00m, "hub.jpg", "USB-C Hub (6-in-1)", "usb-c-hub-6-in-1" },
                    { "f5a0fd9a-35f3-4b57-b5ff-e230eb7a301d", new DateTime(2025, 5, 31, 8, 15, 8, 938, DateTimeKind.Utc).AddTicks(4261), new DateTime(2025, 5, 27, 8, 15, 8, 938, DateTimeKind.Utc).AddTicks(4260), 159.99m, "headphones.jpg", "Noise-Cancelling Headphones", "nc-headphones" },
                    { "fa06c4c9-a3e5-4903-959b-c5f9ae97c452", new DateTime(2025, 6, 9, 8, 15, 8, 938, DateTimeKind.Utc).AddTicks(4210), new DateTime(2025, 5, 26, 8, 15, 8, 938, DateTimeKind.Utc).AddTicks(4209), 59.99m, "webcam.jpg", "1080p Webcam Pro", "webcam-pro-1080p" }
                });
        }
    }
}
