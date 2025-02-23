using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Services.Encounters.Strategies;

namespace Tavernkeep.Core.Services.Encounters
{
	public interface IEncounterServiceStrategies
	{
		public IReadOnlyDictionary<EncounterParticipantType, IAddEncounterParticipantStrategy> AddParticipant { get; }
	}
}
