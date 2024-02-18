using Tavernkeep.Core.Entities;

namespace Tavernkeep.Core.Repositories
{
    public interface IMessageRepository : IRepositoryBase<Message, Guid> 
    {
        Task PurgeMessagesAsync(CancellationToken cancellationToken = default);
        Task<List<Message>> GetMessagesChunkAsync(int skip, int take, CancellationToken cancellationToken = default);
    }
}
