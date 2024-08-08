using MediatR;
using Tavernkeep.Application.Interfaces;
using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Entities.Pathfinder;
using Tavernkeep.Core.Exceptions;
using Tavernkeep.Core.Repositories;
using Tavernkeep.Infrastructure.Notifications.Notifications;

namespace Tavernkeep.Application.UseCases.Characters.Commands.EditHealth
{
	public class EditHealthCommandHandler(
		IUserRepository userRepository,
		ICharacterRepository characterRepository,
		INotificationService notificationService
		) : IRequestHandler<EditHealthCommand, Health>
	{
		public async Task<Health> Handle(EditHealthCommand request, CancellationToken cancellationToken)
		{
			var initiator = await userRepository.FindAsync(request.InitiatorId, cancellationToken: cancellationToken)
				?? throw new BusinessLogicException("User with specified ID doesn't exist.");

			var character = await characterRepository.GetFullCharacterAsync(request.CharacterId, cancellationToken)
				?? throw new BusinessLogicException("Character with specified ID doesn't exist.");

			if (character.Owner.Id != request.InitiatorId && initiator.Role != UserRole.Master)
				throw new InsufficientPermissionException("You do not have the necessary permissions to perform this operation.");

			// TODO: Switch to expceptions
			// Health rules:
			// 1 - Max and temporary can't be bellow zero
			// 2 - Current must be in range [0; Max]
			character.Health.Max = Math.Max(0, request.Max);
			character.Health.Current = Math.Clamp(request.Current, 0, character.Health.Max);
			character.Health.Temporary = Math.Max(0, request.Temporary);

			characterRepository.Save(character);
			await characterRepository.CommitAsync(cancellationToken);
			await notificationService.QueueCharacterNotificationAsync(new CharacterEditedNotification(character), cancellationToken);

			return character.Health;
		}
	}
}
