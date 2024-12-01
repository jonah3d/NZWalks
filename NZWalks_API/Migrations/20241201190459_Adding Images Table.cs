using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NZWalks_API.Migrations
{
    /// <inheritdoc />
    public partial class AddingImagesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("13481ca6-5adf-4d76-aa9d-3182bded63e8"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("5aab4896-c41a-4b6b-9ae9-9303effa8a82"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("f0c54f92-fd53-4df1-a329-063aaf089b9d"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("172beacb-f46c-48b0-ac07-492e2adf7559"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("3a2c466d-4da2-41b4-944b-6b6e8d6d7c6d"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("433d0c6f-d3d4-4d9f-b3e2-b017d5bc0e34"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("b19f23e1-4d5a-4e68-aaf2-a64e1262dc7b"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("c154e40d-6aac-41c6-94e7-6d59ddea950f"));

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileExtension = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileSizeInBytes = table.Column<long>(type: "bigint", nullable: false),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.InsertData(
                table: "Difficulties",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("13481ca6-5adf-4d76-aa9d-3182bded63e8"), "Easy" },
                    { new Guid("5aab4896-c41a-4b6b-9ae9-9303effa8a82"), "Hard" },
                    { new Guid("f0c54f92-fd53-4df1-a329-063aaf089b9d"), "Medium" }
                });

            migrationBuilder.InsertData(
                table: "Regions",
                columns: new[] { "Id", "Code", "Name", "RegionImageurl" },
                values: new object[,]
                {
                    { new Guid("172beacb-f46c-48b0-ac07-492e2adf7559"), "HKB", "Hawke's Bay", "https://www.doc.govt.nz/globalassets/images/conservation/parks-and-recreation/places-to-visit/hawkes-bay/hawkes-bay/hawkes-bay-landscape-1.jpg" },
                    { new Guid("3a2c466d-4da2-41b4-944b-6b6e8d6d7c6d"), "GIS", "Gisborne", "https://www.doc.govt.nz/globalassets/images/conservation/parks-and-recreation/places-to-visit/gisborne/gisborne/gisborne-landscape-1.jpg" },
                    { new Guid("433d0c6f-d3d4-4d9f-b3e2-b017d5bc0e34"), "BOP", "Bay of Plenty", "https://www.doc.govt.nz/globalassets/images/conservation/parks-and-recreation/places-to-visit/bay-of-plenty/bay-of-plenty/bay-of-plenty-landscape-1.jpg" },
                    { new Guid("b19f23e1-4d5a-4e68-aaf2-a64e1262dc7b"), "WKO", "Waikato", "https://www.doc.govt.nz/globalassets/images/conservation/parks-and-recreation/places-to-visit/waikato/waikato/waikato-landscape-1.jpg" },
                    { new Guid("c154e40d-6aac-41c6-94e7-6d59ddea950f"), "AUK", "Auckland", "https://www.doc.govt.nz/globalassets/images/conservation/parks-and-recreation/places-to-visit/auckland/auckland/auckland-landscape-1.jpg" }
                });
        }
    }
}
