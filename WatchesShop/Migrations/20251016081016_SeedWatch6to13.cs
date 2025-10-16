using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WatchesShop.Migrations
{
    /// <inheritdoc />
    public partial class SeedWatch6to13 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Watches",
                columns: new[] { "WatchId", "AssemblyFactory", "BrandId", "CaseId", "GenderId", "ImagePath", "MechanismTypeId", "Model", "Price", "StyleId" },
                values: new object[,]
                {
                    { 4, "Japan", 5, 2, 1, "photo_watch/4.jpg", 2, "X100", 5400m, 1 },
                    { 5, "Switzerland", 3, 7, 2, "photo_watch/5.jpg", 1, "Omega-2025", 23500m, 2 },
                    { 6, "Netherlands", 6, 12, 2, "photo_watch/6.jpg", 1, "A1DF6E", 11110m, 1 },
                    { 7, "Thailand", 3, 13, 3, "photo_watch/7.jpg", 1, "GRT00-1A1ER", 12340m, 3 },
                    { 8, "India", 5, 8, 1, "photo_watch/8.jpg", 2, "96A207", 16700m, 2 },
                    { 9, "Japan", 5, 9, 1, "photo_watch/9.jpg", 1, "96A215", 7689m, 3 },
                    { 10, "Switzerland", 7, 10, 2, "photo_watch/10.jpg", 1, "305SWBL", 21360m, 2 },
                    { 11, "China", 7, 11, 2, "photo_watch/11.jpg", 1, "334SBLBL-WORLD", 18900m, 1 },
                    { 12, "Switzerland", 6, 12, 1, "photo_watch/12.jpg", 1, "A168WG-9E", 11110m, 2 },
                    { 13, "Thailand", 6, 13, 1, "photo_watch/13.jpg", 1, "GA-2100-1A1ER", 12340m, 3 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Watches",
                keyColumn: "WatchId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Watches",
                keyColumn: "WatchId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Watches",
                keyColumn: "WatchId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Watches",
                keyColumn: "WatchId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Watches",
                keyColumn: "WatchId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Watches",
                keyColumn: "WatchId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Watches",
                keyColumn: "WatchId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Watches",
                keyColumn: "WatchId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Watches",
                keyColumn: "WatchId",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Watches",
                keyColumn: "WatchId",
                keyValue: 13);
        }
    }
}
