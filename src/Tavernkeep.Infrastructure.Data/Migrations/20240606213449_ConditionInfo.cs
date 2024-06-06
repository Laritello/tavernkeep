using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tavernkeep.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class ConditionInfo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Conditions",
                table: "Characters",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ConditionInfo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    SecondaryId = table.Column<Guid>(type: "TEXT", nullable: true),
                    Modifiers = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConditionInfo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConditionInfo_ConditionInfo_SecondaryId",
                        column: x => x.SecondaryId,
                        principalTable: "ConditionInfo",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ConditionInfo_SecondaryId",
                table: "ConditionInfo",
                column: "SecondaryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConditionInfo");

            migrationBuilder.DropColumn(
                name: "Conditions",
                table: "Characters");
        }
    }
}
