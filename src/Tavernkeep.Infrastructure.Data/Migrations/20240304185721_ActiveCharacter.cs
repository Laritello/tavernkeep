using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tavernkeep.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class ActiveCharacter : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ActiveCharacterId",
                table: "Users",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_ActiveCharacterId",
                table: "Users",
                column: "ActiveCharacterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Characters_ActiveCharacterId",
                table: "Users",
                column: "ActiveCharacterId",
                principalTable: "Characters",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Characters_ActiveCharacterId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_ActiveCharacterId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ActiveCharacterId",
                table: "Users");
        }
    }
}
