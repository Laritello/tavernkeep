using Tavernkeep.Core.Entities;
using Tavernkeep.Core.Repositories;
using Tavernkeep.Infrastructure.Data.Context;

namespace Tavernkeep.Infrastructure.Data.Repositories
{
    public class RefreshTokenEFRepository(SessionContext context) : EntityFrameworkRepository<RefreshToken>(context), IRefreshTokenRepository
    {
    }
}
