using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Services.Encounters;
using Tavernkeep.Core.Services.Encounters.Strategies;

namespace Tavernkeep.Application.Strategies.Encounters
{
	public class EncounterServiceStrategies(
		IEnumerable<IAddEncounterParticipantStrategy> addParticipantStrategies,
		IEnumerable<IFillEncounterParticipantStrategy> fillParticipantStrategies,
		IEnumerable<IRollEncounterParticipantInitiativeStrategy> rollInitiativeStrategies
		) : IEncounterServiceStrategies
	{
		#region Backing fields

		private readonly Dictionary<EncounterParticipantType, IAddEncounterParticipantStrategy> _addParticipant = 
			addParticipantStrategies.ToDictionary(x => x.Type);

		private readonly Dictionary<EncounterParticipantType, IFillEncounterParticipantStrategy> _fillParticipant =
			fillParticipantStrategies.ToDictionary(x => x.Type);

		private readonly Dictionary<EncounterParticipantType, IRollEncounterParticipantInitiativeStrategy> _rollParticipantInitiative =
			rollInitiativeStrategies.ToDictionary(x => x.Type);

		#endregion

		#region Properties

		public IReadOnlyDictionary<EncounterParticipantType, IAddEncounterParticipantStrategy> AddParticipant => 
			_addParticipant.AsReadOnly();

		public IReadOnlyDictionary<EncounterParticipantType, IFillEncounterParticipantStrategy> FillParticipant => 
			_fillParticipant.AsReadOnly();

		public IReadOnlyDictionary<EncounterParticipantType, IRollEncounterParticipantInitiativeStrategy> RollParticipantInitiative => 
			_rollParticipantInitiative.AsReadOnly();

		#endregion
	}
}
