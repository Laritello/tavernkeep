using MediatR;
using Tavernkeep.Core.Entities;
using Tavernkeep.Core.Repositories;

namespace Tavernkeep.Application.Actions.Chat.Queries.GetMessages
{
    public class GetMessagesQueryHandler(IMessageRepository repository) : IRequestHandler<GetMessagesQuery, List<Message>>
    {
        public async Task<List<Message>> Handle(GetMessagesQuery request, CancellationToken cancellationToken)
        {
            var messages = await repository.GetMessagesChunkAsync(request.Skip, request.Take, cancellationToken);
            return messages;
        }
    }
}
