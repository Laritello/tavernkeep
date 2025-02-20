using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tavernkeep.Core.Entities.Pathfinder.Conditions;

namespace Tavernkeep.Infrastructure.Data.Configuration.ConditionRecords
{
	public class CharacterConditionRecordConfiguration : IEntityTypeConfiguration<CharacterConditionRecord>
	{
		public void Configure(EntityTypeBuilder<CharacterConditionRecord> builder)
		{
			builder.HasOne(c => c.Character)
				.WithMany(c => c.Conditions);
		}
	}
}
