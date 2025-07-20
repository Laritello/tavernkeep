using Tavernkeep.Domain.Entities.Messages;
using Tavernkeep.Domain.Specifications;

namespace Tavernkeep.Domain.Repositories
{
	public interface IMessageRepository : IGuidRepositoryBase<Message, Guid>
	{
		Task PurgeMessagesAsync(CancellationToken cancellationToken = default);
		Task<List<Message>> GetMessagesChunkAsync(int skip, int take, ISpecification<Message> specification = default!, CancellationToken cancellationToken = default);
	}
}
