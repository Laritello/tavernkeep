using MediatR;
using Tavernkeep.Core.Entities.Messages;
using Tavernkeep.Core.Notifications;

namespace Tavernkeep.Application.UseCases.Chat.Notifications.MessageDeleted
{
	public class MessageDeletedNotification(Message message) : INotification, IMessageNotification
	{
		public Message Message { get; set; } = message;
	}
}
