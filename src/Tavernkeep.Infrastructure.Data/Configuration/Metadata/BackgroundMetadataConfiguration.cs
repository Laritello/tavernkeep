﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text.Json;
using Tavernkeep.Core.Entities.Pathfinder.Backgrounds;
using Tavernkeep.Core.Entities.Pathfinder.Builds.Advancements;

namespace Tavernkeep.Infrastructure.Data.Configuration.Metadata
{
	internal class BackgroundMetadataConfiguration : IEntityTypeConfiguration<BackgroundMetadata>
	{
		public void Configure(EntityTypeBuilder<BackgroundMetadata> builder)
		{
			builder.HasKey(a => a.Name);
			builder.Property(a => a.Name).IsRequired();
			builder.Property(a => a.Description).IsRequired();

			builder.OwnsMany(a => a.Progression, b =>
			{
				b.Property(p => p.Advancements)
				.HasConversion(
					v => JsonSerializer.Serialize(v, JsonSerializerOptions.Default),
					v => JsonSerializer.Deserialize<List<Advancement>>(v, JsonSerializerOptions.Default) ?? new List<Advancement>(),
					new ValueComparer<List<Advancement>>(
						(c1, c2) => c1!.SequenceEqual(c2!),
						c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
						c => c.ToList()));

				b.ToJson();
			});
		}
	}
}
