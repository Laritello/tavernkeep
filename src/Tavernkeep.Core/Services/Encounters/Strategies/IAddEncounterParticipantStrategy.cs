using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Entities.Encounters;

namespace Tavernkeep.Core.Services.Encounters.Strategies
{
	public interface IAddEncounterParticipantStrategy
	{
		public EncounterParticipantType Type { get; }
		Task AddParticipantAsync(Encounter encounter, Guid entityId, CancellationToken cancellationToken);
	}
}
