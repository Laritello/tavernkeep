using Tavernkeep.Core.Entities.Pathfinder;

namespace Tavernkeep.Core.Repositories
{
	public interface ICharacterRepository : IGuidRepositoryBase<Character, Guid>
	{
		public Task<List<Character>> GetAllCharactersAsync(CancellationToken cancellationToken = default);
		public Task<Character?> GetFullCharacterAsync(Guid id, CancellationToken cancellationToken = default);
	}
}
