using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tavernkeep.Domain.Entities.Encounters;

namespace Tavernkeep.Infrastructure.Data.Configuration;

internal class EncounterConfiguration : IEntityTypeConfiguration<Encounter>
{
	public void Configure(EntityTypeBuilder<Encounter> builder)
	{
		builder.HasKey(x => x.Id);

		builder.Property(e => e.Name).IsRequired().HasDefaultValue("New encounter");
		builder.Property(e => e.Created).IsRequired().HasDefaultValue(new DateTimeOffset(new DateTime(2025, 1, 1), TimeSpan.Zero));

		builder
			.HasMany(e => e.Participants)
			.WithOne(e => e.Encounter);
	}
}
