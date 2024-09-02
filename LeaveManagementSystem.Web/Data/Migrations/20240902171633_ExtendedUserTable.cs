using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LeaveManagementSystem.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class ExtendedUserTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateOnly>(
                name: "DateOfBirth",
                table: "AspNetUsers",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7a709332-5d98-43e2-8dfc-414711b163a8",
                columns: new[] { "ConcurrencyStamp", "DateOfBirth", "FirstName", "LastName", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e3f58667-7519-402a-bf95-cbe41a6bf463", new DateOnly(1950, 12, 1), "Default", "Admin", "AQAAAAIAAYagAAAAEHg3YxQopw4Cu3LzncdxE2SYSVPXMpLeO263AhL2HIk6T5fpd2sAf0QLNivw+TD0mA==", "a924acfd-e89d-40c6-b4e8-3db2eb75b5ab" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7a709332-5d98-43e2-8dfc-414711b163a8",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "73752bd9-4eeb-49da-b923-ff84b23a51f9", "AQAAAAIAAYagAAAAEHqvoCUNnZ2uYB/o9q1MAcuYhx/AbgPyCFTC466tfO+XxUmJPzUuKRHvxYWOxiOPTg==", "5414faa7-8e6b-4cfb-9b2c-2e054c8dcb01" });
        }
    }
}
