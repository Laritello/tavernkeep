﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tavernkeep.Core.Entities.Pathfinder.Conditions;

namespace Tavernkeep.Infrastructure.Data.Configuration.Library
{
	public class ConditionInformationConfiguration : IEntityTypeConfiguration<ConditionInformation>
	{
		public void Configure(EntityTypeBuilder<ConditionInformation> builder)
		{
			builder.HasKey(c => c.Name);
			builder.Property(c => c.Name).IsRequired();
			builder.Property(c => c.Description).IsRequired();
			builder.Property(c => c.HasLevels).IsRequired();

			builder.OwnsMany(c => c.Related, b =>
			{
				b.OwnsMany(c => c.Modifiers, b => b.ToJson());
				b.ToJson();
			});

			builder.OwnsMany(c => c.Modifiers, b => b.ToJson());
		}
	}
}
