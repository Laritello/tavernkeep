using Tavernkeep.Core.Contracts.Enums;

namespace Tavernkeep.Core.Strategies.Encounters
{
	public interface IEncounterServiceStrategies
	{
		public IReadOnlyDictionary<EncounterParticipantType, IAddEncounterParticipantStrategy> AddParticipant { get; }
		public IReadOnlyDictionary<EncounterParticipantType, IFillEncounterParticipantStrategy> FillParticipant { get; }
		public IReadOnlyDictionary<EncounterParticipantType, IRollEncounterParticipantInitiativeStrategy> RollParticipantInitiative { get; }
		public IReadOnlyDictionary<EncounterParticipantType, IEncounterParticipantConditionStrategy> Conditions { get; }
	}
}
