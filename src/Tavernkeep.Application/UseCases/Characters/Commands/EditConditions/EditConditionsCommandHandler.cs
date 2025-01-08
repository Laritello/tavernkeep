using MediatR;
using Tavernkeep.Application.Interfaces;
using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Exceptions;
using Tavernkeep.Core.Repositories;

namespace Tavernkeep.Application.UseCases.Characters.Commands.EditConditions
{
	public class EditConditionsCommandHandler(
		IUserRepository userRepository,
		ICharacterRepository characterRepository,
		IConditionMetadataRepository conditionRepository,
		INotificationService notificationService
		) : IRequestHandler<EditConditionsCommand>
	{
		public async Task Handle(EditConditionsCommand request, CancellationToken cancellationToken)
		{
			var initiator = await userRepository.FindAsync(request.InitiatorId, cancellationToken: cancellationToken)
				?? throw new BusinessLogicException("User with specified ID doesn't exist.");

			var character = await characterRepository.GetFullCharacterAsync(request.CharacterId, cancellationToken)
				?? throw new BusinessLogicException("Character with specified ID doesn't exist.");

			if (character.Owner.Id != request.InitiatorId && initiator.Role != UserRole.Master)
				throw new InsufficientPermissionException("You do not have the necessary permissions to perform this operation.");

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
					var conditionMetadata = await conditionRepository.GetConditionAsync(condition.Name, cancellationToken)
						?? throw new BusinessLogicException("Condition with specified name doesn't exist.");

					character.Conditions.Add(conditionMetadata.ToCondition(condition.Level));
				}
			}

			await characterRepository.CommitAsync(cancellationToken);
			await notificationService.QueueCharacterNotificationAsync(character, cancellationToken);
		}
	}
}
