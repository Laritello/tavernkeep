using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Services.Encounters.Strategies;

namespace Tavernkeep.Core.Services.Encounters
{
	public interface IEncounterServiceStrategies
	{
		public IReadOnlyDictionary<EncounterParticipantType, IAddEncounterParticipantStrategy> AddParticipant { get; }
		public IReadOnlyDictionary<EncounterParticipantType, IFillEncounterParticipantStrategy> FillParticipant { get; }
		public IReadOnlyDictionary<EncounterParticipantType, IRollEncounterParticipantInitiativeStrategy> RollParticipantInitiative { get; }
	}
}
