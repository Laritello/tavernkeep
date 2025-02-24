using Tavernkeep.Application.Interfaces;
using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Entities.Encounters.Participants;
using Tavernkeep.Core.Entities.Pathfinder.Conditions;
using Tavernkeep.Core.Exceptions;
using Tavernkeep.Core.Repositories;
using Tavernkeep.Core.Strategies.Encounters;

namespace Tavernkeep.Application.Strategies.Encounters.Conditions
{
	public class CharacterEncounterParticipantConditionsStrrategy(
		ICharacterService characterService,
		IConditionLibraryRepository conditionRepository
		) : IEncounterParticipantConditionStrategy
	{
		public EncounterParticipantType Type => EncounterParticipantType.Character;

		public async Task AddConditionToParticipant(EncounterParticipant participant, string conditionName, CancellationToken cancellationToken)
		{
			if (participant is CharacterEncounterParticipant characterParticipant)
			{
				if (characterParticipant.Character.Conditions.Any(x => x.Condition.Name == conditionName))
				{
					return;
				}

				var condition = await conditionRepository.GetConditionAsync(conditionName, cancellationToken) ??
					throw new BusinessLogicException("Condition with provided name does not exist.");

				characterParticipant.Character.AddCondition(new CharacterConditionRecord()
				{
					Condition = condition,
					Character = characterParticipant.Character,
					Level = condition.HasLevels ? 1 : null,
				});

				await characterService.SaveCharacter(characterParticipant.Character, cancellationToken);
			}
		}
	}
}
