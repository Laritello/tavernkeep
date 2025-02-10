using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tavernkeep.Core.Entities.Encounters.Participants;

namespace Tavernkeep.Infrastructure.Data.Configuration.EncounterParticipants
{
	internal class EncounterParticipantConfiguration : IEntityTypeConfiguration<EncounterParticipant>
	{
		public void Configure(EntityTypeBuilder<EncounterParticipant> builder)
		{
			builder.UseTphMappingStrategy();

			builder.HasKey(m => m.Id);
		}
	}
}
