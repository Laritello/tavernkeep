using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tavernkeep.Core.Entities.Encounters.Participants;

namespace Tavernkeep.Infrastructure.Data.Configuration.EncounterParticipants
{
	internal class CharacterEncounterParticipantConfiguration : IEntityTypeConfiguration<CharacterEncounterParticipant>
	{
		public void Configure(EntityTypeBuilder<CharacterEncounterParticipant> builder)
		{
			builder
				.HasOne(c => c.Character)
				.WithMany();
		}
	}
}
