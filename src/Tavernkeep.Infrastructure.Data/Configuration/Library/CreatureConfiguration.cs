using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tavernkeep.Domain.Entities.Library.Creatures;

namespace Tavernkeep.Infrastructure.Data.Configuration.Library
{
	public class CreatureConfiguration : IEntityTypeConfiguration<Creature>
	{
		public void Configure(EntityTypeBuilder<Creature> builder)
		{
			builder.HasKey(c => c.Id);
		}
	}
}
