using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using WatchesShop.Data;

#nullable disable

namespace WatchesShop.Migrations
{
    [DbContext(typeof(WatchContext))]
    [Migration("20251016073500_SeedLookups")]
    public partial class SeedLookups : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Colors",
                columns: new[] { "ColorId", "Name" },
                columnTypes: new[] { "int", "nvarchar(max)" },
                values: new object[,]
                {
                    { 1, "Black" },
                    { 2, "Silver" },
                    { 3, "Gold" },
                    { 4, "Blue" },
                    { 5, "White" },
                    { 6, "Rose Gold" }
                });

            migrationBuilder.InsertData(
                table: "Materials",
                columns: new[] { "MaterialId", "Name" },
                columnTypes: new[] { "int", "nvarchar(max)" },
                values: new object[,]
                {
                    { 1, "Stainless Steel" },
                    { 2, "Titanium" },
                    { 3, "Ceramic" },
                    { 4, "Resin" },
                    { 5, "Gold Plated" },
                    { 6, "Leather" }
                });

            migrationBuilder.InsertData(
                table: "Genders",
                columns: new[] { "GenderId", "Name" },
                columnTypes: new[] { "int", "nvarchar(max)" },
                values: new object[,]
                {
                    { 1, "Male" },
                    { 2, "Female" },
                    { 3, "Unisex" }
                });

            migrationBuilder.InsertData(
                table: "Styles",
                columns: new[] { "StyleId", "Name" },
                columnTypes: new[] { "int", "nvarchar(max)" },
                values: new object[,]
                {
                    { 1, "Classic" },
                    { 2, "Sport" },
                    { 3, "Casual" }
                });

            migrationBuilder.InsertData(
                table: "Mechanisms",
                columns: new[] { "MechanismTypeId", "Name" },
                columnTypes: new[] { "int", "nvarchar(max)" },
                values: new object[,]
                {
                    { 1, "Quartz" },
                    { 2, "Automatic" },
                    { 3, "Mechanical" }
                });

            migrationBuilder.InsertData(
                table: "Brands",
                columns: new[] { "BrandId", "Name" },
                columnTypes: new[] { "int", "nvarchar(max)" },
                values: new object[,]
                {
                    { 1, "Brand 1" }, { 2, "Brand 2" }, { 3, "Brand 3" }, { 4, "Brand 4" },
                    { 5, "Brand 5" }, { 6, "Brand 6" }, { 7, "Brand 7" }, { 8, "Brand 8" },
                    { 9, "Brand 9" }, { 10, "Brand 10" }, { 11, "Brand 11" }, { 12, "Brand 12" },
                    { 13, "Brand 13" }, { 14, "Brand 14" }, { 15, "Brand 15" }, { 16, "Brand 16" },
                    { 17, "Brand 17" }
                });

            // Cases посилаються на MaterialId і ColorId, тому вставляються останніми
            migrationBuilder.InsertData(
                table: "Cases",
                columns: new[] { "CaseId", "Diameter", "MaterialId", "WaterResistance", "LengthCase", "Thickness", "Weight", "ColorId" },
                columnTypes: new[] { "int", "decimal(5,2)", "int", "decimal(5,2)", "decimal(5,2)", "decimal(5,2)", "decimal(5,2)", "int" },
                values: new object[,]
                {
                    { 1, 38.0m, 1, 5.0m,  200.0m, 9.5m,  60.0m, 1 },
                    { 2, 40.0m, 2, 10.0m, 210.0m, 10.0m, 65.0m, 2 },
                    { 3, 42.0m, 1, 20.0m, 215.0m, 11.0m, 70.0m, 3 },
                    { 4, 36.0m, 3, 5.0m,  190.0m, 8.5m,  45.0m, 4 },
                    { 5, 44.0m, 2, 30.0m, 220.0m, 12.0m, 90.0m, 5 },
                    { 6, 40.0m, 4, 10.0m, 200.0m, 9.0m,  55.0m, 6 },
                    { 7, 41.0m, 1, 15.0m, 205.0m, 10.5m, 75.0m, 1 },
                    { 8, 39.0m, 5, 5.0m,  195.0m, 8.0m,  50.0m, 3 },
                    { 9, 43.0m, 2, 20.0m, 210.0m, 11.5m, 80.0m, 2 },
                    { 10, 40.0m, 1, 10.0m, 200.0m, 9.5m,  60.0m, 4 },
                    { 11, 42.0m, 6, 3.0m,  205.0m, 8.0m,  40.0m, 5 },
                    { 12, 44.0m, 2, 20.0m, 215.0m, 11.0m, 85.0m, 6 },
                    { 13, 45.0m, 4, 20.0m, 220.0m, 13.0m, 95.0m, 1 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(table: "Cases", keyColumn: "CaseId",
                keyValues: new object[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13 });

            migrationBuilder.DeleteData(table: "Brands", keyColumn: "BrandId",
                keyValues: new object[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17 });

            migrationBuilder.DeleteData(table: "Mechanisms", keyColumn: "MechanismTypeId",
                keyValues: new object[] { 1, 2, 3 });

            migrationBuilder.DeleteData(table: "Styles", keyColumn: "StyleId",
                keyValues: new object[] { 1, 2, 3 });

            migrationBuilder.DeleteData(table: "Genders", keyColumn: "GenderId",
                keyValues: new object[] { 1, 2, 3 });

            migrationBuilder.DeleteData(table: "Materials", keyColumn: "MaterialId",
                keyValues: new object[] { 1, 2, 3, 4, 5, 6 });

            migrationBuilder.DeleteData(table: "Colors", keyColumn: "ColorId",
                keyValues: new object[] { 1, 2, 3, 4, 5, 6 });
        }
    }
}
