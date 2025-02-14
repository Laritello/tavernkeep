using Tavernkeep.Core.Contracts.Enums;

namespace Tavernkeep.Core.Contracts.Encounters.Dtos
{
	public class EncounterDto
	{
		public Guid Id { get; set; }
		public required string Name { get; set; }
		public EncounterStatus Status { get; set; }
		public required ICollection<EncounterParticipantDto> Participants { get; set; }
	}
}
