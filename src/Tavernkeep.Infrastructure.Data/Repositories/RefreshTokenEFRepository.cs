using Microsoft.EntityFrameworkCore;
using Tavernkeep.Core.Entities;
using Tavernkeep.Core.Repositories;
using Tavernkeep.Infrastructure.Data.Context;

namespace Tavernkeep.Infrastructure.Data.Repositories
{
	public class RefreshTokenEFRepository(SessionContext context) : EntityFrameworkRepository<RefreshToken>(context), IRefreshTokenRepository
	{
		public async Task<List<RefreshToken>> GetExpiredTokensAsync(CancellationToken cancellationToken = default)
		{
			return await AsQueryable().Where(x => x.Expires < DateTime.UtcNow).ToListAsync(cancellationToken);
		}

		public async Task<List<RefreshToken>> GetTokensForUserAsync(Guid userId, CancellationToken cancellationToken = default)
		{
			return await AsQueryable().Where(x => x.UserId == userId).ToListAsync(cancellationToken);
		}
	}
}
