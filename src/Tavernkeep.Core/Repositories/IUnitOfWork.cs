using Tavernkeep.Core.EntityFramework.Context;

namespace Tavernkeep.Core.Repositories
{
    public interface IUnitOfWork
    {
        SessionContext Context { get; }

        Task CommitAsync(CancellationToken cancellationToken = default);

        IUserRepository UserRepository { get; }
        ICharacterRepository CharacterRepository { get; }
    }
}
