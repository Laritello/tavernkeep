using Tavernkeep.Core.Entities;

namespace Tavernkeep.Core.Repositories
{
    public interface IUserRepository : IRepositoryBase<User, Guid>
    {
        Task<User?> GetUserByLogin(string login, CancellationToken cancellationToken = default);
    }
}
