using MediatR;
using Tavernkeep.Core.Entities.Messages;

namespace Tavernkeep.Application.UseCases.Notifications.Queries.NotifyRollMessage
{
	public class NotifyRollMessageQuery(RollMessage message) : IRequest
	{
		public RollMessage Message { get; set; } = message;
	}
}
