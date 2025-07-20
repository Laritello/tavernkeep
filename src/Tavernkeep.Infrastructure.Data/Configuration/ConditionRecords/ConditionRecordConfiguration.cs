using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tavernkeep.Domain.Entities.Pathfinder.Conditions;

namespace Tavernkeep.Infrastructure.Data.Configuration.ConditionRecords
{
	public class ConditionRecordConfiguration : IEntityTypeConfiguration<ConditionRecord>
	{
		public void Configure(EntityTypeBuilder<ConditionRecord> builder)
		{
			builder.UseTphMappingStrategy();

			builder.HasKey(x => x.Id);

			builder.HasOne(c => c.Condition)
				.WithMany();
		}
	}
}
