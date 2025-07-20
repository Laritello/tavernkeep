using MediatR;
using Tavernkeep.Domain.Entities.Messages;
using Tavernkeep.Domain.Notifications;

namespace Tavernkeep.Application.UseCases.Chat.Notifications.RollMessageSent;

public class RollMessageSentNotification(RollMessage message) : INotification, IMessageNotification
{
	public RollMessage Message { get; set; } = message;
}
