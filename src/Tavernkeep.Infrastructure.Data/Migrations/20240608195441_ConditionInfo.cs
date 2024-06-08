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
                name: "CondtionInfo",
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
                    table.PrimaryKey("PK_CondtionInfo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CondtionInfo_CondtionInfo_SecondaryId",
                        column: x => x.SecondaryId,
                        principalTable: "CondtionInfo",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CondtionInfo_SecondaryId",
                table: "CondtionInfo",
                column: "SecondaryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CondtionInfo");

            migrationBuilder.DropColumn(
                name: "Conditions",
                table: "Characters");
        }
    }
}
