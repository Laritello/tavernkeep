using Tavernkeep.Domain.Entities;

namespace Tavernkeep.Domain.Repositories;

public interface IRefreshTokenRepository : IGuidRepositoryBase<RefreshToken, Guid>
{
	Task<List<RefreshToken>> GetTokensForUserAsync(Guid userId, CancellationToken cancellationToken = default);
	Task<List<RefreshToken>> GetExpiredTokensAsync(CancellationToken cancellationToken = default);
}
