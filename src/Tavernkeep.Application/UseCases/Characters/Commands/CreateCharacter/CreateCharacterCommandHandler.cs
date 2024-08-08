using MediatR;
using Tavernkeep.Application.Interfaces;
using Tavernkeep.Core.Entities;
using Tavernkeep.Core.Exceptions;
using Tavernkeep.Core.Repositories;
using Tavernkeep.Infrastructure.Notifications.Notifications;

namespace Tavernkeep.Application.UseCases.Characters.Commands.CreateCharacter
{
	public class CreateCharacterCommandHandler(
		IUserRepository userRepository,
		ICharacterService characterService,
		INotificationService notificationService
		) : IRequestHandler<CreateCharacterCommand, Character>
	{
		public async Task<Character> Handle(CreateCharacterCommand request, CancellationToken cancellationToken)
		{
			var user = await userRepository.FindAsync(request.OwnerId)
				?? throw new BusinessLogicException("Owner with specified ID doesn't exist.");

			var character = await characterService.CreateCharacterAsync(user, request.Name, cancellationToken);
			await notificationService.QueueCharacterNotification(new CharacterEditedNotification(character));

			return character;
		}
	}
}
