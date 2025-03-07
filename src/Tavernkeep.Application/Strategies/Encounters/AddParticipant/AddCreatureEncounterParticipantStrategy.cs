using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Entities.Encounters;
using Tavernkeep.Core.Entities.Encounters.Participants;
using Tavernkeep.Core.Exceptions;
using Tavernkeep.Core.Repositories;
using Tavernkeep.Core.Strategies.Encounters;

namespace Tavernkeep.Application.Strategies.Encounters.AddParticipant
{
	public class AddCreatureEncounterParticipantStrategy(ICreatureLibraryRepository creatureRepository) : IAddEncounterParticipantStrategy
	{
		public EncounterParticipantType Type => EncounterParticipantType.Creature;
		public async Task AddParticipantAsync(Encounter encounter, Guid entityId, CancellationToken cancellationToken)
		{
			var creature = await creatureRepository.GetCreatureAsync(entityId, cancellationToken)
				?? throw new BusinessLogicException("Creature with specified ID not found");

			CreatureEncounterParticipant participant = new()
			{
				Encounter = encounter,
				Origin = creature,
				CurrentHealth = creature.Health,
				TemporaryHealth = 0,
			};

			encounter.AddParticipant(participant);
		}
	}
}
