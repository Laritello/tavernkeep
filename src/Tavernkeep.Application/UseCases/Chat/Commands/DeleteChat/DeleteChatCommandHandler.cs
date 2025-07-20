using MediatR;
using Tavernkeep.Application.UseCases.Chat.Notifications.ChatDeleted;
using Tavernkeep.Domain.Repositories;
using Tavernkeep.Domain.Services;

namespace Tavernkeep.Application.UseCases.Chat.Commands.DeleteChat;

public class DeleteChatCommandHandler(
	IMessageRepository messageRepository,
	INotificationService notificationService
	) : IRequestHandler<DeleteChatCommand>
{
	public async Task Handle(DeleteChatCommand request, CancellationToken cancellationToken)
	{
		await messageRepository.PurgeMessagesAsync(cancellationToken);
		await notificationService.Publish(new ChatDeletedNotification(), cancellationToken);
	}
}
