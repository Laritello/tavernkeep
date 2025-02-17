using Tavernkeep.Core.Entities.Pathfinder;

namespace Tavernkeep.Core.Repositories
{
	public interface ICreatureLibraryRepository : IGuidRepositoryBase<Creature, Guid>
	{
		public Task<List<Creature>> GetAllCreaturesAsync(CancellationToken cancellationToken = default);
	}
}
