using MediatR;
using Tavernkeep.Domain.Entities.Messages;
using Tavernkeep.Domain.Notifications;

namespace Tavernkeep.Application.UseCases.Chat.Notifications.MessageDeleted;

public class MessageDeletedNotification(Message message) : INotification, IMessageNotification
{
	public Message Message { get; set; } = message;
}
