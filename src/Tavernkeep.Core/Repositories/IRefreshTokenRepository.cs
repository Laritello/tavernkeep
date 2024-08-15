using Tavernkeep.Core.Entities;

namespace Tavernkeep.Core.Repositories
{
	public interface IRefreshTokenRepository : IGuidRepositoryBase<RefreshToken, Guid>
	{
		Task<List<RefreshToken>> GetTokensForUserAsync(Guid userId, CancellationToken cancellationToken = default);
		Task<List<RefreshToken>> GetExpiredTokensAsync(CancellationToken cancellationToken = default);
	}
}
