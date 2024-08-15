using Tavernkeep.Core.Entities.Pathfinder.Conditions;

namespace Tavernkeep.Core.Repositories
{
	public interface IConditionMetadataRepository : INameRepositoryBase<ConditionMetadata, string>
	{
		public Task<List<ConditionMetadata>> GetAllConditionsAsync(CancellationToken cancellationToken = default);
		public Task<ConditionMetadata> GetConditionAsync(string name, CancellationToken cancellationToken = default);
	}
}
