using Microsoft.EntityFrameworkCore;
using Tavernkeep.Core.Entities;
using Tavernkeep.Core.EntityFramework.Context;
using Tavernkeep.Core.Repositories;

namespace Tavernkeep.Core.EntityFramework.Repositories
{
    public class UserEFRepository(ApplicationContext context) : EntityFrameworkRepository<User>(context), IUserRepository
    {
        public Task<User?> GetUserByLogin(string login, CancellationToken cancellationToken = default)
        {
            return AsQueryable().Where(x => x.Login == login).FirstOrDefaultAsync(cancellationToken);
        }
    }
}
