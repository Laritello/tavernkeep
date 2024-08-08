using MediatR;
using Tavernkeep.Core.Entities.Messages;
using Tavernkeep.Core.Exceptions;
using Tavernkeep.Core.Repositories;
using Tavernkeep.Core.Specifications;
using Tavernkeep.Core.Specifications.Chat;

namespace Tavernkeep.Application.UseCases.Chat.Queries.GetMessages
{
	public class GetMessagesQueryHandler(
		IUserRepository userRepository,
		IMessageRepository repository
		) : IRequestHandler<GetMessagesQuery, List<Message>>
	{
		public async Task<List<Message>> Handle(GetMessagesQuery request, CancellationToken cancellationToken)
		{
			var initiator = await userRepository.FindAsync(request.InitiatorId, cancellationToken: cancellationToken)
				?? throw new BusinessLogicException("User with specified ID doesn't exist.");

			Specification<Message> specification = new ChatSpecification(initiator);

			return await repository.GetMessagesChunkAsync(request.Skip, request.Take, specification, cancellationToken);
		}
	}
}
