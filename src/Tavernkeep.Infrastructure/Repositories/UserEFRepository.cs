using Microsoft.EntityFrameworkCore;
using Tavernkeep.Core.Entities;
using Tavernkeep.Core.Repositories;
using Tavernkeep.Infrastructure.Context;

namespace Tavernkeep.Infrastructure.Repositories
{
    public class UserEFRepository(SessionContext context) : EntityFrameworkRepository<User>(context), IUserRepository
    {
        public Task<User?> GetUserByLogin(string login, CancellationToken cancellationToken = default)
        {
            return AsQueryable().Where(x => x.Login == login).FirstOrDefaultAsync(cancellationToken);
        }
    }
}
