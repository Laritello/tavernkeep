using MediatR;
using Tavernkeep.Application.Interfaces;
using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Entities;
using Tavernkeep.Core.Exceptions;
using Tavernkeep.Core.Repositories;
using Tavernkeep.Infrastructure.Notifications.Notifications;

namespace Tavernkeep.Application.UseCases.Conditions.Commands.ApplyCondition
{
	public class ApplyConditionCommandHandler(
		IUserRepository userRepository,
		ICharacterRepository characterRepository,
		IConditionMetadataRepository conditionRepository,
		INotificationService notificationService
		) : IRequestHandler<ApplyConditionCommand, Character>
	{
		public async Task<Character> Handle(ApplyConditionCommand request, CancellationToken cancellationToken)
		{
			var initiator = await userRepository.FindAsync(request.InitiatorId)
				?? throw new BusinessLogicException("User with specified ID doesn't exist.");

			var character = await characterRepository.GetFullCharacterAsync(request.CharacterId)
				?? throw new BusinessLogicException("Character with specified ID doesn't exist.");

			if (character.Owner.Id != request.InitiatorId && initiator.Role != UserRole.Master)
				throw new InsufficientPermissionException("You do not have the necessary permissions to perform this operation.");

			if (character.Conditions.Any(x => x.Name == request.ConditionName))
			{
				var condition = character.Conditions.First(x => x.Name == request.ConditionName);

				if (condition.HasLevels)
				{
					condition.Level = request.ConditionLevel ?? 1;
				}
			}
			else
			{
				var conditionMetadata = await conditionRepository.GetConditionAsync(request.ConditionName, cancellationToken)
					?? throw new BusinessLogicException("Condition with specified name doesn't exist.");

				var condition = conditionMetadata.ToCondition();
				character.Conditions.Add(condition);
			}

			await characterRepository.CommitAsync(cancellationToken);
			await notificationService.QueueCharacterNotification(new CharacterEditedNotification(character));

			return character;
		}
	}
}
