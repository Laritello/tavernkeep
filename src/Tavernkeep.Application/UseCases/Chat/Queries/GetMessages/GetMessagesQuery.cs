using MediatR;
using Tavernkeep.Core.Contracts.Chat.Dtos;

namespace Tavernkeep.Application.Actions.Chat.Queries.GetMessages
{
    public class GetMessagesQuery : IRequest<List<MessageDto>>
    {
        public Guid InitiatorId { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }

        public GetMessagesQuery(Guid initiatorId, int skip, int take)
        {
            InitiatorId = initiatorId;
            Skip = skip;
            Take = take;
        }
    }
}
