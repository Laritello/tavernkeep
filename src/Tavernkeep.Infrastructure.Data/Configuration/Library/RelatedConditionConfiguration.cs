using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Tavernkeep.Core.Entities.Pathfinder;

namespace Tavernkeep.Infrastructure.Data.Configuration.Library
{
	public class RelatedConditionConfiguration : IEntityTypeConfiguration<RelatedCondition>
	{
		public void Configure(EntityTypeBuilder<RelatedCondition> builder)
		{
			builder.HasKey(x => x.Id);

			builder.HasOne(x => x.Condition)
				.WithMany();
		}
	}
}
