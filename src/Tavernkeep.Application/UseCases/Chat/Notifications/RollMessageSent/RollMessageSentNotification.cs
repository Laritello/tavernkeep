using MediatR;
using Tavernkeep.Core.Entities.Messages;
using Tavernkeep.Core.Notifications;

namespace Tavernkeep.Application.UseCases.Chat.Notifications.RollMessageSent
{
	public class RollMessageSentNotification(RollMessage message) : INotification, IMessageNotification
	{
		public RollMessage Message { get; set; } = message;
	}
}
