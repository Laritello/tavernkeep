using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Services.Encounters;
using Tavernkeep.Core.Services.Encounters.Strategies;

namespace Tavernkeep.Application.Strategies.Encounters
{
	public class EncounterServiceStrategies(IEnumerable<IAddEncounterParticipantStrategy> addParticipantStrategies) : IEncounterServiceStrategies
	{
		#region Backing fields

		private readonly Dictionary<EncounterParticipantType, IAddEncounterParticipantStrategy> _addParticipant = 
			addParticipantStrategies.ToDictionary(x => x.Type);

		#endregion

		#region Properties

		public IReadOnlyDictionary<EncounterParticipantType, IAddEncounterParticipantStrategy> AddParticipant => _addParticipant.AsReadOnly();

		#endregion
	}
}
