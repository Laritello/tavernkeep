using Tavernkeep.Domain.Contracts.Character.Dtos;

namespace Tavernkeep.Domain.Contracts.Encounters.Dtos
{
	public class CharacterEncounterParticipantDto : EncounterParticipantDto
	{
		public required CharacterEncounterDto Character { get; set; }
	}
}
