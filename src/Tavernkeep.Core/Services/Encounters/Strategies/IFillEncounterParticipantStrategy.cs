using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Entities.Encounters.Participants;

namespace Tavernkeep.Core.Services.Encounters.Strategies
{
	public interface IFillEncounterParticipantStrategy
	{
		public EncounterParticipantType Type { get; }
		Task FillParticipantAsync(EncounterParticipant encounter, CancellationToken cancellationToken);
	}
}
