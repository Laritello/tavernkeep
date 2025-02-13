using Tavernkeep.Core.Contracts.Enums;

namespace Tavernkeep.Core.Contracts.Encounters.Dtos
{
	public class EncounterParticipantDto
	{
		public Guid Id { get; set; }
		public Guid EntityId { get; set; }
		public EncounterParticipantType Type { get; set; }
		public int? Initiative { get; set; }
	}
}
