using MediatR;
using Tavernkeep.Core.Entities.Messages;

namespace Tavernkeep.Application.UseCases.Notifications.Queries.NotifyTextMessage
{
	public class NotifyTextMessageQuery(TextMessage message) : IRequest
	{
		public TextMessage Message { get; set; } = message;
	}
}
