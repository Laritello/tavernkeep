using Tavernkeep.Application.Interfaces;
using Tavernkeep.Domain.Contracts.Enums;
using Tavernkeep.Domain.Entities.Encounters.Participants;
using Tavernkeep.Domain.Entities.Pathfinder.Conditions;
using Tavernkeep.Domain.Exceptions;
using Tavernkeep.Domain.Repositories;
using Tavernkeep.Domain.Strategies.Encounters;

namespace Tavernkeep.Application.Strategies.Encounters.Conditions;

public class CharacterEncounterParticipantConditionsStrategy(
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

	public async Task EditConditionOnParticipant(EncounterParticipant participant, string conditionName, int level, CancellationToken cancellationToken)
	{
		if (participant is CharacterEncounterParticipant characterParticipant)
		{
			var condition = characterParticipant.Character.Conditions.FirstOrDefault(x => x.Condition.Name == conditionName);

			if (condition is not null && condition.Condition.HasLevels)
			{
				condition.Level = level;
			}

			await characterService.SaveCharacter(characterParticipant.Character, cancellationToken);
		}
	}

	public async Task DeleteConditionFromParticipant(EncounterParticipant participant, string conditionName, CancellationToken cancellationToken)
	{
		if (participant is CharacterEncounterParticipant characterParticipant)
		{
			var condition = characterParticipant.Character.Conditions.FirstOrDefault(x => x.Condition.Name == conditionName);

			if (condition is not null)
			{
				characterParticipant.Character.RemoveCondition(condition);
			}

			await characterService.SaveCharacter(characterParticipant.Character, cancellationToken);
		}
	}
}
