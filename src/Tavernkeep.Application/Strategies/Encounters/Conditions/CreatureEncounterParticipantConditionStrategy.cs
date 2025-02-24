using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Entities.Encounters.Participants;
using Tavernkeep.Core.Entities.Pathfinder.Conditions;
using Tavernkeep.Core.Exceptions;
using Tavernkeep.Core.Repositories;
using Tavernkeep.Core.Strategies.Encounters;

namespace Tavernkeep.Application.Strategies.Encounters.Conditions
{
	public class CreatureEncounterParticipantConditionsStrrategy(
			IConditionLibraryRepository conditionRepository
			) : IEncounterParticipantConditionStrategy
	{
		public EncounterParticipantType Type => EncounterParticipantType.Creature;

		public async Task AddConditionToParticipant(EncounterParticipant participant, string conditionName, CancellationToken cancellationToken)
		{
			if (participant is CreatureEncounterParticipant creatureParticipant)
			{
				if (creatureParticipant.Conditions.Any(x => x.Condition.Name == conditionName))
				{
					return;
				}

				var condition = await conditionRepository.GetConditionAsync(conditionName, cancellationToken) ??
					throw new BusinessLogicException("Condition with provided name does not exist.");

				creatureParticipant.AddCondition(new CreatureConditionRecord()
				{
					Participant = creatureParticipant,
					Condition = condition,
					Creature = creatureParticipant.Creature,
					Level = condition.HasLevels ? 1 : null,
				});
			}
		}

		public Task DeleteConditionFromParticipant(EncounterParticipant participant, string conditionName, CancellationToken cancellationToken)
		{
			if (participant is CreatureEncounterParticipant creatureParticipant)
			{
				var condition = creatureParticipant.Conditions.FirstOrDefault(x => x.Condition.Name == conditionName);

				if (condition is not null)
				{
					creatureParticipant.RemoveCondition(condition);
				}
			}

			return Task.CompletedTask;
		}
	}
}
