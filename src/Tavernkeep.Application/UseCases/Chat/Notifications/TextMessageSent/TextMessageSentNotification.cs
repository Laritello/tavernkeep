using MediatR;
using Tavernkeep.Core.Entities.Messages;
using Tavernkeep.Core.Notifications;

namespace Tavernkeep.Application.UseCases.Chat.Notifications.TextMessageSent
{
	public class TextMessageSentNotification(TextMessage message) : INotification, IMessageNotification
	{
		public TextMessage Message { get; set; } = message;
	}
}
