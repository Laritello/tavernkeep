using Tavernkeep.Domain.Contracts.Enums;
using Tavernkeep.Domain.Entities.Encounters.Participants;

namespace Tavernkeep.Domain.Strategies.Encounters;

public interface IFillEncounterParticipantStrategy
{
	public EncounterParticipantType Type { get; }
	Task FillParticipantAsync(EncounterParticipant encounter, CancellationToken cancellationToken);
}
