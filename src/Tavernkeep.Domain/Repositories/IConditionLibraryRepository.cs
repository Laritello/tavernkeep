using Tavernkeep.Domain.Entities.Library.Conditions;

namespace Tavernkeep.Domain.Repositories
{
	public interface IConditionLibraryRepository : IStringRepositoryBase<Condition, string>
	{
		public Task<List<Condition>> GetAllConditionsAsync(CancellationToken cancellationToken = default);
		public Task<Condition> GetConditionAsync(string name, CancellationToken cancellationToken = default);
	}
}
