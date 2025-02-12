using Tavernkeep.Core.Contracts.Enums;

namespace Tavernkeep.Core.Contracts.Encounters.Requests
{
	public class AddEncounterParticipantRequest
	{
		public EncounterParticipantType Type { get; set; }
		public Guid EntityId { get; set; }
	}
}
