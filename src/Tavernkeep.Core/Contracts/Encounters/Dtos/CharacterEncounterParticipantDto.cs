using Tavernkeep.Core.Contracts.Character.Dtos;

namespace Tavernkeep.Core.Contracts.Encounters.Dtos
{
	public class CharacterEncounterParticipantDto : EncounterParticipantDto
	{
		public required CharacterEncounterDto Character { get; set; }
	}
}
