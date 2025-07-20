using MediatR;
using Tavernkeep.Application.Interfaces;
using Tavernkeep.Domain.Entities.Pathfinder.Conditions;
using Tavernkeep.Domain.Exceptions;
using Tavernkeep.Domain.Repositories;

namespace Tavernkeep.Application.UseCases.Characters.Commands.EditConditions
{
	public class EditConditionsCommandHandler(
		ICharacterService characterService,
		IConditionLibraryRepository conditionRepository
		) : IRequestHandler<EditConditionsCommand>
	{
		public async Task Handle(EditConditionsCommand request, CancellationToken cancellationToken)
		{
			var character = await characterService.RetrieveCharacterForAction(request.CharacterId, request.InitiatorId, cancellationToken);

			// TODO: Switch to dictionary under the hood
			character.Conditions.RemoveAll(x => !request.Conditions.Any(c => c.Name == x.Condition.Name));

			foreach (var condition in request.Conditions)
			{
				var characterCondition = character.Conditions.FirstOrDefault(x => x.Condition.Name == condition.Name);

				if (characterCondition is not null)
				{
					if (characterCondition.Condition.HasLevels)
					{
						characterCondition.Level = condition.Level;
					}
				}
				else
				{
					var conditionInformation = await conditionRepository.GetConditionAsync(condition.Name, cancellationToken)
						?? throw new BusinessLogicException("Condition with specified name doesn't exist.");

					character.Conditions.Add(new CharacterConditionRecord()
					{
						Condition = conditionInformation,
						Character = character,
						Level = conditionInformation.HasLevels ? Math.Max(condition.Level ?? 0, 1) : null,
					});
				}
			}

			await characterService.SaveCharacter(character, cancellationToken);
		}
	}
}
