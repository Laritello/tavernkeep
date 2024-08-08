using Tavernkeep.Core.Entities;

namespace Tavernkeep.Core.Repositories
{
	public interface ICharacterRepository : IRepositoryBase<Character, Guid>
	{
		public Task<List<Character>> GetAllCharactersAsync(CancellationToken cancellationToken = default);
		public Task<Character?> GetFullCharacterAsync(Guid id, CancellationToken cancellationToken = default);
	}
}
