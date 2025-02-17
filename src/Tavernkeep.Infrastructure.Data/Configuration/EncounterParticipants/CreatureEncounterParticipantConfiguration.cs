using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Tavernkeep.Core.Entities.Encounters.Participants;

namespace Tavernkeep.Infrastructure.Data.Configuration.EncounterParticipants
{
	internal class CreatureEncounterParticipantConfiguration : IEntityTypeConfiguration<CreatureEncounterParticipant>
	{
		public void Configure(EntityTypeBuilder<CreatureEncounterParticipant> builder)
		{
			builder
				.HasOne(c => c.Creature)
				.WithMany();
		}
	}
}
