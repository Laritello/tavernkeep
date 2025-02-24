using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tavernkeep.Core.Entities.Pathfinder.Conditions;

namespace Tavernkeep.Infrastructure.Data.Configuration.ConditionRecords
{
	public class CreatureConditionRecordConfiguration : IEntityTypeConfiguration<CreatureConditionRecord>
	{
		public void Configure(EntityTypeBuilder<CreatureConditionRecord> builder)
		{
			builder.HasOne(x => x.Creature)
				.WithMany();
		}
	}
}
