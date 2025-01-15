﻿using MediatR;
using Tavernkeep.Application.Interfaces;
using Tavernkeep.Core.Exceptions;
using Tavernkeep.Core.Repositories;

namespace Tavernkeep.Application.UseCases.Chat.Commands.DeleteMessage
{
	public class DeleteMessageCommandHandler(IMessageRepository messageRepository, INotificationService notificationService) : IRequestHandler<DeleteMessageCommand>
	{
		public async Task Handle(DeleteMessageCommand request, CancellationToken cancellationToken)
		{
			var message = await messageRepository.FindAsync(request.MessageId, cancellationToken: cancellationToken)
				?? throw new BusinessLogicException("Message not found.");

			messageRepository.Remove(message);
			await messageRepository.CommitAsync(cancellationToken);
			await notificationService.QueueDeleteMessageAsync(message, cancellationToken);
		}
	}
}
