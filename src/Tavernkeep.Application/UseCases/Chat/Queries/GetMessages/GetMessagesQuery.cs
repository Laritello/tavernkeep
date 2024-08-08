using MediatR;
using Tavernkeep.Core.Entities.Messages;

namespace Tavernkeep.Application.UseCases.Chat.Queries.GetMessages
{
	public class GetMessagesQuery(Guid initiatorId, int skip, int take) : IRequest<List<Message>>
	{
		public Guid InitiatorId { get; set; } = initiatorId;
		public int Skip { get; set; } = skip;
		public int Take { get; set; } = take;
	}
}
