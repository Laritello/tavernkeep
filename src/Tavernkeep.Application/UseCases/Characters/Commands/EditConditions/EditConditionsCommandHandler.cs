using MediatR;
using Tavernkeep.Application.Interfaces;
using Tavernkeep.Core.Exceptions;
using Tavernkeep.Core.Repositories;

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
			character.Conditions.RemoveAll(x => !request.Conditions.Any(c => c.Name == x.Name));

			foreach (var condition in request.Conditions)
			{
				var characterCondition = character.Conditions.FirstOrDefault(x => x.Name == condition.Name);

				if (characterCondition is not null)
				{
					characterCondition.Level = condition.Level;
				}
				else
				{
					var conditionInformation = await conditionRepository.GetConditionAsync(condition.Name, cancellationToken)
						?? throw new BusinessLogicException("Condition with specified name doesn't exist.");

					character.Conditions.Add(conditionInformation.ToCondition(condition.Level));
				}
			}

			await characterService.SaveCharacter(character, cancellationToken);
		}
	}
}
