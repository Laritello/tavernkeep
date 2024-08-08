using Tavernkeep.Core.Entities.Messages;
using Tavernkeep.Core.Specifications;

namespace Tavernkeep.Core.Repositories
{
	public interface IMessageRepository : IRepositoryBase<Message, Guid>
	{
		Task PurgeMessagesAsync(CancellationToken cancellationToken = default);
		Task<List<Message>> GetMessagesChunkAsync(int skip, int take, ISpecification<Message> specification = default!, CancellationToken cancellationToken = default);
	}
}
