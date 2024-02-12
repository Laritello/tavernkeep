using MediatR;
using Tavernkeep.Core.Entities;

namespace Tavernkeep.Application.Actions.Chat.Queries.GetMessages
{
    public class GetMessagesQuery : IRequest<List<Message>>
    {
        public int Skip { get; set; }
        public int Take { get; set; }

        public GetMessagesQuery(int skip, int take)
        {
            Skip = skip;
            Take = take;
        }
    }
}
