using MediatR;
using Tavernkeep.Application.Interfaces;
using Tavernkeep.Domain.Entities.Pathfinder.Conditions;
using Tavernkeep.Domain.Exceptions;
using Tavernkeep.Domain.Repositories;

namespace Tavernkeep.Application.UseCases.Characters.Commands.PerformLongRest
{
	public class PerformLongRestCommandHandler(
		ICharacterService characterService,
		IConditionLibraryRepository conditionRepository
		) : IRequestHandler<PerformLongRestCommand>
	{
		public async Task Handle(PerformLongRestCommand request, CancellationToken cancellationToken)
		{
			var character = await characterService.RetrieveCharacterForAction(request.CharacterId, request.InitiatorId, cancellationToken);

			/*
			 * The character regains Hit Points equal to their Constitution modifier (minimum 1) multiplied by their level. 
			 * The character loses the fatigued condition.
			 * The character reduces the severity of the doomed and drained conditions by 1.
			 * Most spellcasters need to rest before they regain their spells for the day.
			 * 
			 * If rests without comfort - only half of HP are restored. If sleeps in armor - apply fatigued.
			*/
			var constitution = character.Abilities["Constitution"];

			character.Health.Current += request.RestWithoutComfort
				? constitution.Modifier * character.Level / 2
				: constitution.Modifier * character.Level;

			if (request.SleepInArmor)
			{
				if (!character.Conditions.Any(x => x.Condition.Name == "Fatigued"))
				{
					var condition = await conditionRepository.GetConditionAsync("Fatigued", cancellationToken)
						?? throw new BusinessLogicException("Condition with specified name doesn't exist.");

					character.Conditions.Add(new CharacterConditionRecord
					{
						Condition = condition,
						Character = character,
					});
				}
			}
			else
			{
				character.Conditions.RemoveAll(x => x.Condition.Name == "Fatigued");
			}

			// Using to list call, because we might want to delete condition for the collection
			// and this will lead to an error. Performance hit is negligible since rarely character has more than 2-3 conditions
			// at once.
			foreach (var condition in character.Conditions.Where(x => x.Condition.Name is "Doomed" or "Drained").ToList())
			{
				if (condition.Level == 1)
				{
					character.Conditions.Remove(condition);
				}
				else
				{
					condition.Level -= 1;
				}
			}

			await characterService.SaveCharacter(character, cancellationToken);
		}
	}
}
