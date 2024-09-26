using MediatR;
using Tavernkeep.Application.Interfaces;
using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Entities.Pathfinder.Properties;
using Tavernkeep.Core.Exceptions;
using Tavernkeep.Core.Repositories;

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
			if (request.Max < 0)
				throw new BusinessLogicException($"{nameof(request.Max)} can't be below zero.");

			if (request.Current < 0)
				throw new BusinessLogicException($"{nameof(request.Current)} can't be below zero.");

			if (request.Temporary < 0)
				throw new BusinessLogicException($"{nameof(request.Temporary)} can't be below zero.");

			var initiator = await userRepository.FindAsync(request.InitiatorId, cancellationToken: cancellationToken)
				?? throw new BusinessLogicException("User with specified ID doesn't exist.");

			var character = await characterRepository.GetFullCharacterAsync(request.CharacterId, cancellationToken)
				?? throw new BusinessLogicException("Character with specified ID doesn't exist.");

			if (character.Owner.Id != request.InitiatorId && initiator.Role != UserRole.Master)
				throw new InsufficientPermissionException("You do not have the necessary permissions to perform this operation.");

			character.Health.Current = request.Current;
			character.Health.Temporary = request.Temporary;

			characterRepository.Save(character);
			await characterRepository.CommitAsync(cancellationToken);
			await notificationService.QueueCharacterNotificationAsync(character, cancellationToken);

			return character.Health;
		}
	}
}
