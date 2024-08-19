using AutoMapper;
using MediatR;
using Tavernkeep.Application.Interfaces;
using Tavernkeep.Core.Contracts.Character.Dtos;
using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Entities.Pathfinder.Properties;
using Tavernkeep.Core.Exceptions;
using Tavernkeep.Core.Repositories;
using Tavernkeep.Infrastructure.Notifications.Notifications;

namespace Tavernkeep.Application.UseCases.Characters.Commands.ModifyHealth
{
	public class ModifyHealthCommandHandler(
		IUserRepository userRepository,
		ICharacterRepository characterRepository,
		INotificationService notificationService,
		IMapper mapper
		) : IRequestHandler<ModifyHealthCommand, Health>
	{
		public async Task<Health> Handle(ModifyHealthCommand request, CancellationToken cancellationToken)
		{
			var initiator = await userRepository.FindAsync(request.InitiatorId, cancellationToken: cancellationToken)
				?? throw new BusinessLogicException("User with specified ID doesn't exist.");

			var character = await characterRepository.GetFullCharacterAsync(request.CharacterId, cancellationToken)
				?? throw new BusinessLogicException("Character with specified ID doesn't exist.");

			if (character.Owner.Id != request.InitiatorId && initiator.Role != UserRole.Master)
				throw new InsufficientPermissionException("You do not have the necessary permissions to perform this operation.");

			if (request.Change > 0)
				character.Health.Heal(request.Change);
			else
				character.Health.Damage(Math.Abs(request.Change));

			characterRepository.Save(character);
			await characterRepository.CommitAsync(cancellationToken);
			await notificationService.QueueCharacterNotificationAsync(new CharacterEditedNotification(mapper.Map<CharacterDto>(character)), cancellationToken);

			return character.Health;
		}
	}
}
