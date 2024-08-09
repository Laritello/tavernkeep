using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tavernkeep.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class ArmorAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("49786f33-2671-4705-9a4f-5570584c6f5c"));

            migrationBuilder.AddColumn<string>(
                name: "Armor",
                table: "Characters",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "ActiveCharacterId", "Login", "Password", "Role" },
                values: new object[] { new Guid("84b01c0e-810d-4150-b0e6-b352653be15b"), null, "admin", "admin", "Master" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("84b01c0e-810d-4150-b0e6-b352653be15b"));

            migrationBuilder.DropColumn(
                name: "Armor",
                table: "Characters");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "ActiveCharacterId", "Login", "Password", "Role" },
                values: new object[] { new Guid("49786f33-2671-4705-9a4f-5570584c6f5c"), null, "admin", "admin", "Master" });
        }
    }
}
