using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tavernkeep.Core.Entities.Encounters;

namespace Tavernkeep.Infrastructure.Data.Configuration
{
	internal class EncounterConfiguration : IEntityTypeConfiguration<Encounter>
	{
		public void Configure(EntityTypeBuilder<Encounter> builder)
		{
			builder.HasKey(x => x.Id);

			builder.Property(e => e.Name).IsRequired().HasDefaultValue("New encounter");

			builder
				.HasMany(e => e.Participants)
				.WithOne(e => e.Encounter);
		}
	}
}
