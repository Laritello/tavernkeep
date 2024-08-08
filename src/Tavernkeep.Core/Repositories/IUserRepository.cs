using Tavernkeep.Core.Entities;

namespace Tavernkeep.Core.Repositories
{
	public interface IUserRepository : IRepositoryBase<User, Guid>
	{
		Task<List<User>> GetAllUsersAsync(CancellationToken cancellationToken = default);
		Task<User?> GetUserByLoginAsync(string login, CancellationToken cancellationToken = default);
	}
}
