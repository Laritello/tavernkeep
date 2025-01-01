using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tavernkeep.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class CharacterClassAndAncestry : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Ancestry",
                table: "Characters",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Class",
                table: "Characters",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ancestry",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "Class",
                table: "Characters");
        }
    }
}
