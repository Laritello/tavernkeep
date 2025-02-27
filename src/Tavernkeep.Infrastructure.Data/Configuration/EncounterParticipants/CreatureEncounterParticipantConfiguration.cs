using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tavernkeep.Core.Entities.Encounters.Participants;

namespace Tavernkeep.Infrastructure.Data.Configuration.EncounterParticipants
{
	internal class CreatureEncounterParticipantConfiguration : IEntityTypeConfiguration<CreatureEncounterParticipant>
	{
		public void Configure(EntityTypeBuilder<CreatureEncounterParticipant> builder)
		{
			builder
				.HasOne(c => c.Origin)
				.WithMany();

			builder.HasMany(c => c.Conditions)
				.WithOne(c => c.Participant);
		}
	}
}
