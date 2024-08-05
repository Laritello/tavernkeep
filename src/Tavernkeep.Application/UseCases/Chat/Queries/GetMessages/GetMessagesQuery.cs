using MediatR;
using Tavernkeep.Core.Entities.Messages;

namespace Tavernkeep.Application.UseCases.Chat.Queries.GetMessages
{
    public class GetMessagesQuery : IRequest<List<Message>>
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
