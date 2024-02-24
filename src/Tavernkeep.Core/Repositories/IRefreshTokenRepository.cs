using Tavernkeep.Core.Entities;

namespace Tavernkeep.Core.Repositories
{
    public interface IRefreshTokenRepository : IRepositoryBase<RefreshToken, Guid>
    {
        Task<List<RefreshToken>> GetTokensForUser(Guid userId, CancellationToken cancellationToken = default!);
    }
}
