﻿using AutoMapper;
using MediatR;
using Tavernkeep.Application.Interfaces;
using Tavernkeep.Core.Contracts.Character.Dtos;
using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Entities.Pathfinder;
using Tavernkeep.Core.Exceptions;
using Tavernkeep.Core.Repositories;
using Tavernkeep.Infrastructure.Notifications.Notifications;

namespace Tavernkeep.Application.UseCases.Conditions.Commands.RemoveCondition
{
	public class RemoveConditionCommandHandler(
		IUserRepository userRepository,
		ICharacterRepository characterRepository,
		INotificationService notificationService,
		IMapper mapper
		) : IRequestHandler<RemoveConditionCommand, Character>
	{
		public async Task<Character> Handle(RemoveConditionCommand request, CancellationToken cancellationToken)
		{
			var initiator = await userRepository.FindAsync(request.InitiatorId, cancellationToken: cancellationToken)
				?? throw new BusinessLogicException("User with specified ID doesn't exist.");

			var character = await characterRepository.GetFullCharacterAsync(request.CharacterId, cancellationToken)
				?? throw new BusinessLogicException("Character with specified ID doesn't exist.");

			if (character.Owner.Id != request.InitiatorId && initiator.Role != UserRole.Master)
				throw new InsufficientPermissionException("You do not have the necessary permissions to perform this operation.");

			if (!character.Conditions.Any(x => x.Name == request.ConditionName))
				throw new BusinessLogicException($"Unable to remove condition {request.ConditionName}. The following condition isn't applied to selected character.");

			character.Conditions.RemoveAll(x => x.Name == request.ConditionName);

			await characterRepository.CommitAsync(cancellationToken);
			await notificationService.QueueCharacterNotificationAsync(new CharacterEditedNotification(mapper.Map<CharacterDto>(character)), cancellationToken);

			return character;
		}
	}
}
