using Tavernkeep.Core.Entities;

namespace Tavernkeep.Core.Repositories
{
    public interface ICharacterRepository : IRepositoryBase<Character, Guid> 
    {
        public Task<Character?> GetFullCharacter(Guid id, CancellationToken cancellationToken = default);
    }
}
