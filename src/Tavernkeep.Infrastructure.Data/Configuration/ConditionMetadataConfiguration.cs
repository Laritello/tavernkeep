using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tavernkeep.Core.Entities.Conditions;

namespace Tavernkeep.Infrastructure.Data.Configuration
{
	public class ConditionMetadataConfiguration : IEntityTypeConfiguration<ConditionMetadata>
	{
		public void Configure(EntityTypeBuilder<ConditionMetadata> builder)
		{
			builder.HasKey(c => c.Id);
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
