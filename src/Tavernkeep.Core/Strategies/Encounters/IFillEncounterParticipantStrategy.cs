using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Entities.Encounters.Participants;

namespace Tavernkeep.Core.Strategies.Encounters
{
	public interface IFillEncounterParticipantStrategy
	{
		public EncounterParticipantType Type { get; }
		Task FillParticipantAsync(EncounterParticipant encounter, CancellationToken cancellationToken);
	}
}
