using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tavernkeep.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedCreatureParticipant : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CreatureId",
                table: "EncounterParticipant",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EncounterParticipant_CreatureId",
                table: "EncounterParticipant",
                column: "CreatureId");

            migrationBuilder.AddForeignKey(
                name: "FK_EncounterParticipant_LibraryCreature_CreatureId",
                table: "EncounterParticipant",
                column: "CreatureId",
                principalTable: "LibraryCreature",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EncounterParticipant_LibraryCreature_CreatureId",
                table: "EncounterParticipant");

            migrationBuilder.DropIndex(
                name: "IX_EncounterParticipant_CreatureId",
                table: "EncounterParticipant");

            migrationBuilder.DropColumn(
                name: "CreatureId",
                table: "EncounterParticipant");
        }
    }
}
