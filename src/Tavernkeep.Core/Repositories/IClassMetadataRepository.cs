using Tavernkeep.Core.Entities.Pathfinder.Classes;

namespace Tavernkeep.Core.Repositories
{
	public interface IClassMetadataRepository : INameRepositoryBase<ClassMetadata, string> 
	{
		Task<List<ClassMetadata>> GetAllClassesAsync(CancellationToken cancellationToken = default);
	}
}
