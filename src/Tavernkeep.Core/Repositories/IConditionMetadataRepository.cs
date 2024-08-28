using Tavernkeep.Core.Entities.Pathfinder.Conditions;

namespace Tavernkeep.Core.Repositories
{
	public interface IConditionMetadataRepository : IStringRepositoryBase<ConditionTemplate, string>
	{
		public Task<List<ConditionTemplate>> GetAllConditionsAsync(CancellationToken cancellationToken = default);
		public Task<ConditionTemplate> GetConditionAsync(string id, CancellationToken cancellationToken = default);
	}
}
