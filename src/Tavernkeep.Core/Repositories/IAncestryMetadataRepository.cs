using Tavernkeep.Core.Entities.Pathfinder.Ancestries;

namespace Tavernkeep.Core.Repositories
{
	public interface IAncestryMetadataRepository : INameRepositoryBase<AncestryMetadata, string> 
	{
		Task<List<AncestryMetadata>> GetAllAncestriesAsync(CancellationToken cancellationToken = default);
	}
}
