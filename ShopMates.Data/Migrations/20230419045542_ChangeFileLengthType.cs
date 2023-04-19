using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShopMates.Data.Migrations
{
    public partial class ChangeFileLengthType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("410cd1a8-43a2-42f2-88be-bcd3727d0f27"),
                column: "ConcurrencyStamp",
                value: "2c9420c7-5d91-41db-bd3f-8e43ee2e71f2");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("8f81b6c3-9468-4b62-b178-18ae55daad62"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "bfc834b4-7a6d-4c99-9240-893b63bba8b6", "AQAAAAEAACcQAAAAEASqjuMAwhXLnEPNOB6+1fstyZT7zpZrArwkiwofcLCpXJ8Z9HnuYt5nhtE1MTtUgw==" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2023, 4, 19, 11, 55, 41, 933, DateTimeKind.Local).AddTicks(6660));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTime(2023, 4, 19, 11, 55, 41, 933, DateTimeKind.Local).AddTicks(6735));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("410cd1a8-43a2-42f2-88be-bcd3727d0f27"),
                column: "ConcurrencyStamp",
                value: "d1776718-6df8-4052-a943-4b437c0e06cb");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("8f81b6c3-9468-4b62-b178-18ae55daad62"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "ea9ac7f7-78fa-4774-b9e8-bedbf08cec5f", "AQAAAAEAACcQAAAAELHSB7xwhMWW6l28WSkgkJ9Yao77VpfaldcnrwVogywIIfmAh23AlfFIMTAeVaaHbA==" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2023, 4, 14, 14, 38, 54, 796, DateTimeKind.Local).AddTicks(6810));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTime(2023, 4, 14, 14, 38, 54, 796, DateTimeKind.Local).AddTicks(6847));
        }
    }
}
