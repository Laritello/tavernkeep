using MediatR;
using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Entities.Messages;
using Tavernkeep.Core.Exceptions;
using Tavernkeep.Core.Repositories;
using Tavernkeep.Core.Specifications;
using Tavernkeep.Core.Specifications.Chat;

namespace Tavernkeep.Application.Actions.Chat.Queries.GetMessages
{
    public class GetMessagesQueryHandler(IUserRepository userRepository, IMessageRepository repository) : IRequestHandler<GetMessagesQuery, List<Message>>
    {
        public async Task<List<Message>> Handle(GetMessagesQuery request, CancellationToken cancellationToken)
        {
            var initiator = await userRepository.FindAsync(request.InitiatorId)
                ?? throw new BusinessLogicException("User with specified ID doesn't exist.");

            Specification<Message> specification = initiator.Role == UserRole.Master
                ? new MasterChatSpecification(initiator.Id)
                : new PlayerChatSpecification(initiator.Id);

            var messages = await repository.GetMessagesChunkAsync(request.Skip, request.Take, specification, cancellationToken);
            return messages;
        }
    }
}
