using AutoMapper;
using MediatR;
using Tavernkeep.Application.Interfaces;
using Tavernkeep.Core.Contracts.Character.Dtos;
using Tavernkeep.Core.Entities.Pathfinder;
using Tavernkeep.Core.Exceptions;
using Tavernkeep.Core.Repositories;
using Tavernkeep.Infrastructure.Notifications.Notifications;

namespace Tavernkeep.Application.UseCases.Characters.Commands.CreateCharacter
{
	public class CreateCharacterCommandHandler(
		IUserRepository userRepository,
		ICharacterService characterService,
		INotificationService notificationService,
		IMapper mapper
		) : IRequestHandler<CreateCharacterCommand, Character>
	{
		public async Task<Character> Handle(CreateCharacterCommand request, CancellationToken cancellationToken)
		{
			var user = await userRepository.FindAsync(request.OwnerId, cancellationToken: cancellationToken)
				?? throw new BusinessLogicException("Owner with specified ID doesn't exist.");

			var character = await characterService.CreateCharacterAsync(user, request.Name, cancellationToken);
			await notificationService.QueueCharacterNotificationAsync(new CharacterEditedNotification(mapper.Map<CharacterDto>(character)), cancellationToken);

			return character;
		}
	}
}
