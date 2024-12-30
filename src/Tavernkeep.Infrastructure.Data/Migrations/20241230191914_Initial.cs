using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tavernkeep.Infrastructure.Data.Migrations
{
	/// <inheritdoc />
	public partial class Initial : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable(
				name: "Conditions",
				columns: table => new
				{
					Name = table.Column<string>(type: "TEXT", nullable: false),
					Description = table.Column<string>(type: "TEXT", nullable: false),
					HasLevels = table.Column<bool>(type: "INTEGER", nullable: false),
					Level = table.Column<int>(type: "INTEGER", nullable: false),
					Id = table.Column<string>(type: "TEXT", nullable: true),
					Modifiers = table.Column<string>(type: "TEXT", nullable: true),
					Related = table.Column<string>(type: "TEXT", nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Conditions", x => x.Name);
				});

			migrationBuilder.CreateTable(
				name: "RefreshTokens",
				columns: table => new
				{
					Id = table.Column<Guid>(type: "TEXT", nullable: false),
					UserId = table.Column<Guid>(type: "TEXT", nullable: false),
					Token = table.Column<string>(type: "TEXT", nullable: false),
					Expires = table.Column<DateTime>(type: "TEXT", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_RefreshTokens", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "Characters",
				columns: table => new
				{
					Id = table.Column<Guid>(type: "TEXT", nullable: false),
					OwnerId = table.Column<Guid>(type: "TEXT", nullable: false),
					Name = table.Column<string>(type: "TEXT", nullable: false),
					Level = table.Column<int>(type: "INTEGER", nullable: false),
					Acrobatics = table.Column<string>(type: "TEXT", nullable: false),
					Arcana = table.Column<string>(type: "TEXT", nullable: false),
					Armor = table.Column<string>(type: "TEXT", nullable: false),
					Athletics = table.Column<string>(type: "TEXT", nullable: false),
					Charisma = table.Column<string>(type: "TEXT", nullable: false),
					Conditions = table.Column<string>(type: "TEXT", nullable: true),
					Constitution = table.Column<string>(type: "TEXT", nullable: false),
					Crafting = table.Column<string>(type: "TEXT", nullable: false),
					Deception = table.Column<string>(type: "TEXT", nullable: false),
					Dexterity = table.Column<string>(type: "TEXT", nullable: false),
					Diplomacy = table.Column<string>(type: "TEXT", nullable: false),
					Fortitude = table.Column<string>(type: "TEXT", nullable: false),
					Health = table.Column<string>(type: "TEXT", nullable: false),
					Intelligence = table.Column<string>(type: "TEXT", nullable: false),
					Intimidation = table.Column<string>(type: "TEXT", nullable: false),
					Lores = table.Column<string>(type: "TEXT", nullable: true),
					Medicine = table.Column<string>(type: "TEXT", nullable: false),
					Nature = table.Column<string>(type: "TEXT", nullable: false),
					Occultism = table.Column<string>(type: "TEXT", nullable: false),
					Perception = table.Column<string>(type: "TEXT", nullable: false),
					Performance = table.Column<string>(type: "TEXT", nullable: false),
					Reflex = table.Column<string>(type: "TEXT", nullable: false),
					Religion = table.Column<string>(type: "TEXT", nullable: false),
					Society = table.Column<string>(type: "TEXT", nullable: false),
					Stealth = table.Column<string>(type: "TEXT", nullable: false),
					Strength = table.Column<string>(type: "TEXT", nullable: false),
					Survival = table.Column<string>(type: "TEXT", nullable: false),
					Thievery = table.Column<string>(type: "TEXT", nullable: false),
					Will = table.Column<string>(type: "TEXT", nullable: false),
					Wisdom = table.Column<string>(type: "TEXT", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Characters", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "Users",
				columns: table => new
				{
					Id = table.Column<Guid>(type: "TEXT", nullable: false),
					Login = table.Column<string>(type: "TEXT", nullable: false),
					Password = table.Column<string>(type: "TEXT", nullable: false),
					Role = table.Column<string>(type: "TEXT", nullable: false),
					ActiveCharacterId = table.Column<Guid>(type: "TEXT", nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Users", x => x.Id);
					table.ForeignKey(
						name: "FK_Users_Characters_ActiveCharacterId",
						column: x => x.ActiveCharacterId,
						principalTable: "Characters",
						principalColumn: "Id");
				});

			migrationBuilder.CreateTable(
				name: "Messages",
				columns: table => new
				{
					Id = table.Column<Guid>(type: "TEXT", nullable: false),
					SenderId = table.Column<Guid>(type: "TEXT", nullable: false),
					Created = table.Column<DateTime>(type: "TEXT", nullable: false),
					Discriminator = table.Column<string>(type: "TEXT", maxLength: 21, nullable: false),
					RollType = table.Column<string>(type: "TEXT", nullable: true),
					Expression = table.Column<string>(type: "TEXT", nullable: true),
					Text = table.Column<string>(type: "TEXT", nullable: true),
					RecipientId = table.Column<Guid>(type: "TEXT", nullable: true),
					Result = table.Column<string>(type: "TEXT", nullable: true),
					Skill = table.Column<string>(type: "TEXT", nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Messages", x => x.Id);
					table.ForeignKey(
						name: "FK_Messages_Users_RecipientId",
						column: x => x.RecipientId,
						principalTable: "Users",
						principalColumn: "Id");
					table.ForeignKey(
						name: "FK_Messages_Users_SenderId",
						column: x => x.SenderId,
						principalTable: "Users",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateIndex(
				name: "IX_Characters_OwnerId",
				table: "Characters",
				column: "OwnerId");

			migrationBuilder.CreateIndex(
				name: "IX_Messages_RecipientId",
				table: "Messages",
				column: "RecipientId");

			migrationBuilder.CreateIndex(
				name: "IX_Messages_SenderId",
				table: "Messages",
				column: "SenderId");

			migrationBuilder.CreateIndex(
				name: "IX_Users_ActiveCharacterId",
				table: "Users",
				column: "ActiveCharacterId");

			migrationBuilder.CreateIndex(
				name: "IX_Users_Login",
				table: "Users",
				column: "Login",
				unique: true);

			migrationBuilder.AddForeignKey(
				name: "FK_Characters_Users_OwnerId",
				table: "Characters",
				column: "OwnerId",
				principalTable: "Users",
				principalColumn: "Id",
				onDelete: ReferentialAction.Cascade);
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropForeignKey(
				name: "FK_Characters_Users_OwnerId",
				table: "Characters");

			migrationBuilder.DropTable(
				name: "Conditions");

			migrationBuilder.DropTable(
				name: "Messages");

			migrationBuilder.DropTable(
				name: "RefreshTokens");

			migrationBuilder.DropTable(
				name: "Users");

			migrationBuilder.DropTable(
				name: "Characters");
		}
	}
}
