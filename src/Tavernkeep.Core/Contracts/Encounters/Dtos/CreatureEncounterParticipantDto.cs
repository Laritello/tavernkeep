using Tavernkeep.Core.Contracts.Creatures;

namespace Tavernkeep.Core.Contracts.Encounters.Dtos
{
	public class CreatureEncounterParticipantDto : EncounterParticipantDto
	{
		public required string Statblock { get; set; }
	}
}
