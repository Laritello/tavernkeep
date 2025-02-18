using Tavernkeep.Core.Entities.Pathfinder.Conditions;

namespace Tavernkeep.Core.Repositories
{
	public interface IConditionLibraryRepository : IStringRepositoryBase<ConditionInformation, string>
	{
		public Task<List<ConditionInformation>> GetAllConditionsAsync(CancellationToken cancellationToken = default);
		public Task<ConditionInformation> GetConditionAsync(string name, CancellationToken cancellationToken = default);
	}
}
