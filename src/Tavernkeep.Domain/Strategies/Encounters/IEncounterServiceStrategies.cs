using Tavernkeep.Domain.Contracts.Enums;

namespace Tavernkeep.Domain.Strategies.Encounters;

public interface IEncounterServiceStrategies
{
	public IReadOnlyDictionary<EncounterParticipantType, IAddEncounterParticipantStrategy> AddParticipant { get; }
	public IReadOnlyDictionary<EncounterParticipantType, IFillEncounterParticipantStrategy> FillParticipant { get; }
	public IReadOnlyDictionary<EncounterParticipantType, IRollEncounterParticipantInitiativeStrategy> RollParticipantInitiative { get; }
	public IReadOnlyDictionary<EncounterParticipantType, IEncounterParticipantConditionStrategy> Conditions { get; }
}
