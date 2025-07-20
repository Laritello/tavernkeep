using Tavernkeep.Domain.Contracts.Enums;
using Tavernkeep.Domain.Entities.Pathfinder;

namespace Tavernkeep.Domain.Entities.Encounters.Participants;

public class CharacterEncounterParticipant : EncounterParticipant
{
	public Guid CharacterId { get; set; }
	public required Character Character { get; set; }
	public override EncounterParticipantType Type => EncounterParticipantType.Character;
}
