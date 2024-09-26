﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text.Json;
using Tavernkeep.Core.Entities.Pathfinder;
using Tavernkeep.Core.Entities.Pathfinder.Builds;
using Tavernkeep.Core.Entities.Pathfinder.Builds.Snapshots;
using Tavernkeep.Infrastructure.Data.Extensions;

namespace Tavernkeep.Infrastructure.Data.Configuration
{
	public class CharacterConfiguration : IEntityTypeConfiguration<Character>
	{
		public void Configure(EntityTypeBuilder<Character> builder)
		{
			builder.HasKey(c => c.Id);
			builder.HasOne(c => c.Owner).WithMany(u => u.Characters).IsRequired();

			builder.Property(c => c.Name).IsRequired();
			builder.Property(c => c.Build)
				.HasConversion(
				v => JsonSerializer.Serialize(v, JsonSerializerOptions.Default),
				v => JsonSerializer.Deserialize<Build>(v, JsonSerializerOptions.Default) ?? new());

			builder.Property(c => c.Snapshot)
				.HasConversion(
				v => JsonSerializer.Serialize(v, JsonSerializerOptions.Default),
				v => JsonSerializer.Deserialize<CharacterSnapshot>(v, JsonSerializerOptions.Default) ?? new());

			builder.OwnsJson(c => c.Health);
			builder.OwnsOne(c => c.Armor, b =>
			{
				b.OwnsOne(a => a.Proficiencies);
				b.ToJson();
			});

			builder.OwnsJson(c => c.Strength);
			builder.OwnsJson(c => c.Dexterity);
			builder.OwnsJson(c => c.Intelligence);
			builder.OwnsJson(c => c.Constitution);
			builder.OwnsJson(c => c.Wisdom);
			builder.OwnsJson(c => c.Charisma);

			builder.OwnsJson(c => c.Acrobatics);
			builder.OwnsJson(c => c.Arcana);
			builder.OwnsJson(c => c.Athletics);
			builder.OwnsJson(c => c.Crafting);
			builder.OwnsJson(c => c.Deception);
			builder.OwnsJson(c => c.Diplomacy);
			builder.OwnsJson(c => c.Intimidation);
			builder.OwnsJson(c => c.Medicine);
			builder.OwnsJson(c => c.Nature);
			builder.OwnsJson(c => c.Occultism);
			builder.OwnsJson(c => c.Performance);
			builder.OwnsJson(c => c.Religion);
			builder.OwnsJson(c => c.Society);
			builder.OwnsJson(c => c.Stealth);
			builder.OwnsJson(c => c.Survival);
			builder.OwnsJson(c => c.Thievery);

			builder.OwnsMany(c => c.Lores, b => b.ToJson());

			builder.OwnsJson(c => c.Perception);

			builder.OwnsJson(c => c.Fortitude);
			builder.OwnsJson(c => c.Reflex);
			builder.OwnsJson(c => c.Will);

			builder.OwnsMany(c => c.Conditions, b =>
			{
				b.ToJson();
				b.OwnsMany(con => con.Modifiers, b => b.ToJson());
			});
		}
	}
}
