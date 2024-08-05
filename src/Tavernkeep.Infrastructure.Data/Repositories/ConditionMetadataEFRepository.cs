using Microsoft.EntityFrameworkCore;
using Tavernkeep.Core.Entities.Conditions;
using Tavernkeep.Core.Repositories;
using Tavernkeep.Infrastructure.Data.Context;

namespace Tavernkeep.Infrastructure.Data.Repositories
{
	public class ConditionMetadataEFRepository(SessionContext context) : EntityFrameworkRepository<ConditionMetadata>(context), IConditionMetadataRepository
	{
		public Task<List<ConditionMetadata>> GetAllConditionsAsync(CancellationToken cancellationToken = default)
		{
			return AsQueryable().ToListAsync(cancellationToken);
		}

		public Task<ConditionMetadata> GetConditionAsync(string name, CancellationToken cancellationToken = default)
		{
			// TODO: Fix exceptions
			return AsQueryable().Where(x => x.Name == name).FirstAsync(cancellationToken);
		}
	}
}
