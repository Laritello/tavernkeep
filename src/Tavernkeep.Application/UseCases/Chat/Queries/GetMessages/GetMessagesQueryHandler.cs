using AutoMapper;
using MediatR;
using Tavernkeep.Core.Contracts.Chat.Dtos;
using Tavernkeep.Core.Entities.Messages;
using Tavernkeep.Core.Exceptions;
using Tavernkeep.Core.Repositories;
using Tavernkeep.Core.Specifications;
using Tavernkeep.Core.Specifications.Chat;

namespace Tavernkeep.Application.Actions.Chat.Queries.GetMessages
{
    public class GetMessagesQueryHandler(
        IUserRepository userRepository, 
        IMessageRepository repository,
        IMapper mapper
        ) : IRequestHandler<GetMessagesQuery, List<MessageDto>>
    {
        public async Task<List<MessageDto>> Handle(GetMessagesQuery request, CancellationToken cancellationToken)
        {
            var initiator = await userRepository.FindAsync(request.InitiatorId)
                ?? throw new BusinessLogicException("User with specified ID doesn't exist.");

            Specification<Message> specification = new ChatSpecification(initiator);

            var messages = await repository.GetMessagesChunkAsync(request.Skip, request.Take, specification, cancellationToken);
            return mapper.Map<List<MessageDto>>(messages);
        }
    }
}
