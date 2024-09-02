using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LeaveManagementSystem.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedingDefaultRolesandUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "938bfcf3-9072-4fbb-9c0c-87a69d935a2e", null, "Administrator", "ADMINISTRATOR" },
                    { "e9536cf8-0cb2-4fa0-9037-eb3797924347", null, "Supervisor", "SUPERVISOR" },
                    { "eba21fe0-287b-41f3-90f6-994dfb8bedd3", null, "Employee", "EMPLOYEE" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "7a709332-5d98-43e2-8dfc-414711b163a8", 0, "73752bd9-4eeb-49da-b923-ff84b23a51f9", "admin@localhost.com", true, false, null, "ADMIN@LOCALHOST.COM", "ADMIN@LOCALHOST.COM", "AQAAAAIAAYagAAAAEHqvoCUNnZ2uYB/o9q1MAcuYhx/AbgPyCFTC466tfO+XxUmJPzUuKRHvxYWOxiOPTg==", null, false, "5414faa7-8e6b-4cfb-9b2c-2e054c8dcb01", false, "admin@localhost.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "938bfcf3-9072-4fbb-9c0c-87a69d935a2e", "7a709332-5d98-43e2-8dfc-414711b163a8" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e9536cf8-0cb2-4fa0-9037-eb3797924347");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "eba21fe0-287b-41f3-90f6-994dfb8bedd3");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "938bfcf3-9072-4fbb-9c0c-87a69d935a2e", "7a709332-5d98-43e2-8dfc-414711b163a8" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "938bfcf3-9072-4fbb-9c0c-87a69d935a2e");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7a709332-5d98-43e2-8dfc-414711b163a8");
        }
    }
}
