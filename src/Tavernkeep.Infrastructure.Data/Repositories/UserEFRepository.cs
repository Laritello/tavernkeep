using Microsoft.EntityFrameworkCore;
using Tavernkeep.Core.Entities;
using Tavernkeep.Core.Repositories;
using Tavernkeep.Infrastructure.Data.Context;

namespace Tavernkeep.Infrastructure.Data.Repositories
{
    public class UserEFRepository(SessionContext context) : EntityFrameworkRepository<User>(context), IUserRepository
    {
        public Task<List<User>> GetAllUsersAsync(CancellationToken cancellationToken = default)
        {
            return AsQueryable().ToListAsync(cancellationToken);
        }
        public Task<User?> GetUserByLoginAsync(string login, CancellationToken cancellationToken = default)
        {
            return AsQueryable().Where(x => x.Login == login).FirstOrDefaultAsync(cancellationToken);
        }
    }
}
