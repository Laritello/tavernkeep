using Microsoft.EntityFrameworkCore;
using Tavernkeep.Core.Entities.Pathfinder.Conditions;
using Tavernkeep.Core.Repositories;
using Tavernkeep.Infrastructure.Data.Context;
using Tavernkeep.Infrastructure.Data.Repositories.Base;

namespace Tavernkeep.Infrastructure.Data.Repositories
{
	public class ConditionMetadataEFRepository(SessionContext context) : EntityFrameworkNameRepository<ConditionTemplate>(context), IConditionMetadataRepository
	{
		public Task<List<ConditionTemplate>> GetAllConditionsAsync(CancellationToken cancellationToken = default)
		{
			return AsQueryable().ToListAsync(cancellationToken);
		}

		public Task<ConditionTemplate> GetConditionAsync(string name, CancellationToken cancellationToken = default)
		{
			return AsQueryable().Where(x => x.Name == name).FirstAsync(cancellationToken);
		}
	}
}
