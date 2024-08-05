using Tavernkeep.Core.Entities.Conditions;

namespace Tavernkeep.Core.Repositories
{
	public interface IConditionMetadataRepository : IRepositoryBase<ConditionMetadata, Guid>
	{
		public Task<List<ConditionMetadata>> GetAllConditionsAsync(CancellationToken cancellationToken = default);
		public Task<ConditionMetadata> GetConditionAsync(string name, CancellationToken cancellationToken = default);
	}
}
