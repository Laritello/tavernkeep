using MediatR;
using Tavernkeep.Domain.Entities.Messages;
using Tavernkeep.Domain.Notifications;

namespace Tavernkeep.Application.UseCases.Chat.Notifications.TextMessageSent;

public class TextMessageSentNotification(TextMessage message) : INotification, IMessageNotification
{
	public TextMessage Message { get; set; } = message;
}
