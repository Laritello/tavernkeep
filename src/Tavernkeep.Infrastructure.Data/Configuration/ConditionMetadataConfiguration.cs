using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tavernkeep.Core.Entities.Conditions;
using Tavernkeep.Infrastructure.Data.Extensions;

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

			builder.OwnsJson(c => c.Related, b => b.ToJson());
			builder.OwnsJson(c => c.Modifiers, b => b.ToJson());
		}
	}
}
