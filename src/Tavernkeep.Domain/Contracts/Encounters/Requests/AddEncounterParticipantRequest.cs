using Tavernkeep.Domain.Contracts.Enums;

namespace Tavernkeep.Domain.Contracts.Encounters.Requests;

public class AddEncounterParticipantRequest
{
	public EncounterParticipantType Type { get; set; }
	public Guid EntityId { get; set; }
}
