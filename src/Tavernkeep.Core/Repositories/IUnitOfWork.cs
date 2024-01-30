using Tavernkeep.Core.EntityFramework.Context;

namespace Tavernkeep.Core.Repositories
{
    public interface IUnitOfWork
    {
        ApplicationContext Context { get; }

        Task CommitAsync(CancellationToken cancellationToken = default);

        ISessionRepository SessionRepository { get; }
        IUserRepository UserRepository { get; }
        ICharacterRepository CharacterRepository { get; }
    }
}
