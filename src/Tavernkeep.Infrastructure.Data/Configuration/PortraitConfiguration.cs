using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tavernkeep.Core.Entities.Pathfinder;

namespace Tavernkeep.Infrastructure.Data.Configuration
{
	public class PortraitConfiguration : IEntityTypeConfiguration<Portrait>
	{
		public void Configure(EntityTypeBuilder<Portrait> builder)
		{
			builder.HasKey(p => p.Id);

			builder.Property(p => p.Bytes).IsRequired();
		}
	}
}
