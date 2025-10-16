using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WatchesShop.Migrations
{
    /// <inheritdoc />
    public partial class SeedWatches : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Watches",
                columns: new[] { "WatchId", "AssemblyFactory", "BrandId", "CaseId", "GenderId", "ImagePath", "MechanismTypeId", "Model", "Price", "StyleId" },
                values: new object[,]
                {
                    { 1, "Netherlands", 17, 13, 2, "photo_watch/1.jpg", 1, "GA-24560R", 12340m, 3 },
                    { 2, "France", 9, 8, 3, "photo_watch/2.jpg", 2, "95707", 16700m, 2 },
                    { 3, "South Korea", 13, 9, 1, "photo_watch/3.jpg", 3, "RH456", 7689m, 3 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Watches",
                keyColumn: "WatchId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Watches",
                keyColumn: "WatchId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Watches",
                keyColumn: "WatchId",
                keyValue: 3);
        }
    }
}
