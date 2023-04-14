using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShopMates.Data.Migrations
{
    public partial class SeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "IsShowOnHome", "ParentId", "SortOrder", "Status" },
                values: new object[,]
                {
                    { 1, true, null, 1, 1 },
                    { 2, true, null, 2, 1 }
                });

            migrationBuilder.InsertData(
                table: "Languages",
                columns: new[] { "Id", "IsDefault", "Name" },
                values: new object[,]
                {
                    { "en-US", false, "English" },
                    { "vi-VN", true, "Tiếng Việt" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "DateCreated", "IsFeatured", "OriginalPrice", "Price", "Stock" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 4, 14, 13, 20, 50, 78, DateTimeKind.Local).AddTicks(5466), null, 150000m, 2000000m, 15 },
                    { 2, new DateTime(2023, 4, 14, 13, 20, 50, 78, DateTimeKind.Local).AddTicks(5479), null, 150000m, 2000000m, 15 }
                });

            migrationBuilder.InsertData(
                table: "CategoryTranslations",
                columns: new[] { "Id", "CategoryId", "LanguageId", "Name", "SeoAlias", "SeoDescription", "SeoTitle" },
                values: new object[,]
                {
                    { 1, 1, "vi-VN", "Áo Nam", "ao-nam", "Sản phẩm áo thời trang Nam", "Sản phẩm áo thời trang Nam" },
                    { 2, 1, "en-US", "Men Shirt", "men-shirt", "This Shirt product for man", "This Shirt product for man" },
                    { 3, 2, "vi-VN", "Quần Nam", "quan-nam", "Sản phẩm Quần thời trang Nam", "Sản phẩm Quần thời trang Nam" },
                    { 4, 2, "en-US", "Men Trousers", "men-trousers", "This Trousers product for man", "This Trousers product for man" }
                });

            migrationBuilder.InsertData(
                table: "ProductInCategories",
                columns: new[] { "CategoryId", "ProductId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 2 }
                });

            migrationBuilder.InsertData(
                table: "ProductTranslations",
                columns: new[] { "Id", "Description", "Details", "LanguageId", "Name", "ProductId", "SeoAlias", "SeoDescription", "SeoTitle" },
                values: new object[,]
                {
                    { 1, "Áo Sơ Mi Nam Màu Đen", "error-error-Tiến Đẹp Trai-Error-Error", "vi-VN", "Áo Sơ Mi Nam Đen", 1, "ao-so-mi-nam-den", "Áo Sơ Mi Nam Đen", "Áo Sơ Mi Nam Đen" },
                    { 2, "Black Men T-Shirt", "error-error-Tiến Đẹp Trai-Error-Error", "en-US", "Black Men T-Shirt", 1, "black-men-t-shirt", "Black Men T-Shirt", "Black Men T-Shirt" },
                    { 3, "Quần jean Nam Màu Đen", "error-error-Tiến Đẹp Trai-Error-Error", "vi-VN", "Quần jean Nam Đen", 2, "quan-jean-nam-den", "Quần jean Nam Đen", "Quần jean Nam Đen" },
                    { 4, "Black Men Jeans", "error-error-Tiến Đẹp Trai-Error-Error", "en-US", "Black Men Jeans", 2, "black-men-jeans", "Black Men Jeans", "Black Men Jeans" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CategoryTranslations",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "CategoryTranslations",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "CategoryTranslations",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "CategoryTranslations",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "ProductInCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "ProductInCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { 2, 2 });

            migrationBuilder.DeleteData(
                table: "ProductTranslations",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ProductTranslations",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ProductTranslations",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ProductTranslations",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Id",
                keyValue: "en-US");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Id",
                keyValue: "vi-VN");

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
