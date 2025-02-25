using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tavernkeep.Core.Entities.Library.Conditions;

namespace Tavernkeep.Infrastructure.Data.Configuration.Library
{
	public class ConditionRelatedConfiguration : IEntityTypeConfiguration<ConditionRelated>
	{
		public void Configure(EntityTypeBuilder<ConditionRelated> builder)
		{
			builder.HasKey(x => x.Id);

			builder.HasOne(x => x.Condition)
				.WithMany();
		}
	}
}
