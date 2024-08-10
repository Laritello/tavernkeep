using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tavernkeep.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class SavingThrowsAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("84b01c0e-810d-4150-b0e6-b352653be15b"));

            migrationBuilder.AddColumn<string>(
                name: "Fortitude",
                table: "Characters",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Reflex",
                table: "Characters",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Will",
                table: "Characters",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "ActiveCharacterId", "Login", "Password", "Role" },
                values: new object[] { new Guid("f31ec05c-c23e-4195-97ba-3ad59be92f22"), null, "admin", "admin", "Master" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("f31ec05c-c23e-4195-97ba-3ad59be92f22"));

            migrationBuilder.DropColumn(
                name: "Fortitude",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "Reflex",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "Will",
                table: "Characters");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "ActiveCharacterId", "Login", "Password", "Role" },
                values: new object[] { new Guid("84b01c0e-810d-4150-b0e6-b352653be15b"), null, "admin", "admin", "Master" });
        }
    }
}
