using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShopMates.Data.Migrations
{
    public partial class SeedIdentityUserRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AppRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName" },
                values: new object[] { new Guid("410cd1a8-43a2-42f2-88be-bcd3727d0f27"), "d1776718-6df8-4052-a943-4b437c0e06cb", "Adminitrator Role Powerful, Can hack this", "Admin", "Adminitrator" });

            migrationBuilder.InsertData(
                table: "AppUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("410cd1a8-43a2-42f2-88be-bcd3727d0f27"), new Guid("8f81b6c3-9468-4b62-b178-18ae55daad62") });

            migrationBuilder.InsertData(
                table: "AppUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Dob", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("8f81b6c3-9468-4b62-b178-18ae55daad62"), 0, "ea9ac7f7-78fa-4774-b9e8-bedbf08cec5f", new DateTime(1999, 8, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@gmail.com", true, "Tien", "Lam", false, null, "admin@gmail.com", "Adminitrator", "AQAAAAEAACcQAAAAELHSB7xwhMWW6l28WSkgkJ9Yao77VpfaldcnrwVogywIIfmAh23AlfFIMTAeVaaHbA==", null, false, "", false, "Admin" });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("410cd1a8-43a2-42f2-88be-bcd3727d0f27"));

            migrationBuilder.DeleteData(
                table: "AppUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("410cd1a8-43a2-42f2-88be-bcd3727d0f27"), new Guid("8f81b6c3-9468-4b62-b178-18ae55daad62") });

            migrationBuilder.DeleteData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("8f81b6c3-9468-4b62-b178-18ae55daad62"));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2023, 4, 14, 14, 17, 47, 101, DateTimeKind.Local).AddTicks(9647));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTime(2023, 4, 14, 14, 17, 47, 101, DateTimeKind.Local).AddTicks(9662));
        }
    }
}
