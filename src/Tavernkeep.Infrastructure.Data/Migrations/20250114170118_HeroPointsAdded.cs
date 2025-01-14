using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tavernkeep.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class HeroPointsAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HeroPoints",
                table: "Characters",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HeroPoints",
                table: "Characters");
        }
    }
}
