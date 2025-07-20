using Microsoft.EntityFrameworkCore;
using Tavernkeep.Domain.Entities;
using Tavernkeep.Domain.Repositories;
using Tavernkeep.Infrastructure.Data.Context;
using Tavernkeep.Infrastructure.Data.Repositories.Base;

namespace Tavernkeep.Infrastructure.Data.Repositories
{
	public class UserEFRepository(SessionContext context) : EntityFrameworkGuidRepository<User>(context), IUserRepository
	{
		public Task<List<User>> GetAllUsersAsync(CancellationToken cancellationToken = default)
		{
			return AsQueryable().Include(x => x.Characters).Include(x => x.ActiveCharacter).ToListAsync(cancellationToken);
		}

		public Task<User?> GetDetailsAsync(Guid id, CancellationToken cancellationToken = default)
		{
			return AsQueryable().Where(x => x.Id == id).Include(x => x.ActiveCharacter).Include(x => x.Characters).FirstOrDefaultAsync(cancellationToken);
		}

		public Task<User?> GetUserByLoginAsync(string login, CancellationToken cancellationToken = default)
		{
			return AsQueryable().Where(x => x.Login == login).FirstOrDefaultAsync(cancellationToken);
		}
	}
}
