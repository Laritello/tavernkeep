using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tavernkeep.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class SpeedsAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Burrow",
                table: "Characters",
                type: "TEXT",
                nullable: false,
                defaultValue: "{}");

            migrationBuilder.AddColumn<string>(
                name: "Climb",
                table: "Characters",
                type: "TEXT",
                nullable: false,
                defaultValue: "{}");

            migrationBuilder.AddColumn<string>(
                name: "Fly",
                table: "Characters",
                type: "TEXT",
                nullable: false,
                defaultValue: "{}");

            migrationBuilder.AddColumn<string>(
                name: "Swim",
                table: "Characters",
                type: "TEXT",
                nullable: false,
                defaultValue: "{}");

            migrationBuilder.AddColumn<string>(
                name: "Walk",
                table: "Characters",
                type: "TEXT",
                nullable: false,
                defaultValue: "{}");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Burrow",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "Climb",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "Fly",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "Swim",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "Walk",
                table: "Characters");
        }
    }
}
