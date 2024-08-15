using MediatR;
using Tavernkeep.Application.Interfaces;
using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Entities.Pathfinder.Properties;
using Tavernkeep.Core.Exceptions;
using Tavernkeep.Core.Repositories;
using Tavernkeep.Infrastructure.Notifications.Notifications;

namespace Tavernkeep.Application.UseCases.Characters.Commands.EditPerception
{
	public class EditPerceptionCommandHandler(
		IUserRepository userRepository,
		ICharacterRepository characterRepository,
		INotificationService notificationService
		) : IRequestHandler<EditPerceptionCommand, Perception>
	{
		public async Task<Perception> Handle(EditPerceptionCommand request, CancellationToken cancellationToken)
		{
			var initiator = await userRepository.FindAsync(request.InitiatorId, cancellationToken: cancellationToken)
				?? throw new BusinessLogicException("User with specified ID doesn't exist.");

			var character = await characterRepository.GetFullCharacterAsync(request.CharacterId, cancellationToken)
				?? throw new BusinessLogicException("Character with specified ID doesn't exist.");

			if (character.Owner.Id != request.InitiatorId && initiator.Role != UserRole.Master)
				throw new InsufficientPermissionException("You do not have the necessary permissions to perform this operation.");

			character.Perception.Proficiency = request.Proficiency;

			characterRepository.Save(character);
			await characterRepository.CommitAsync(cancellationToken);
			await notificationService.QueueCharacterNotificationAsync(new CharacterEditedNotification(character), cancellationToken);

			return character.Perception;
		}
	}
}
