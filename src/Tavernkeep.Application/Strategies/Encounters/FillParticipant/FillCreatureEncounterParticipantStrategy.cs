using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Entities.Encounters.Participants;
using Tavernkeep.Core.Repositories;
using Tavernkeep.Core.Strategies.Encounters;

namespace Tavernkeep.Application.Strategies.Encounters.FillParticipant
{
	public class FillCreatureEncounterParticipantStrategy(ICreatureLibraryRepository creatureRepository) : IFillEncounterParticipantStrategy
	{
		public EncounterParticipantType Type => EncounterParticipantType.Creature;

		public async Task FillParticipantAsync(EncounterParticipant participant, CancellationToken cancellationToken)
		{
			if (participant is CreatureEncounterParticipant creatureParticipant)
			{
				creatureParticipant.Origin = await creatureRepository.GetCreatureAsync(creatureParticipant.OriginId, cancellationToken);
			}
		}
	}
}
