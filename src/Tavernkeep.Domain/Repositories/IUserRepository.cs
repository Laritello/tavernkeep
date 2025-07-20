using Tavernkeep.Domain.Entities;

namespace Tavernkeep.Domain.Repositories;

public interface IUserRepository : IGuidRepositoryBase<User, Guid>
{
	Task<List<User>> GetAllUsersAsync(CancellationToken cancellationToken = default);
	Task<User?> GetUserByLoginAsync(string login, CancellationToken cancellationToken = default);
	Task<User?> GetDetailsAsync(Guid id, CancellationToken cancellationToken = default);
}
