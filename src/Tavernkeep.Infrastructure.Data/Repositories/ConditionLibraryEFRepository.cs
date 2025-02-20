using Microsoft.EntityFrameworkCore;
using Tavernkeep.Core.Entities.Pathfinder;
using Tavernkeep.Core.Repositories;
using Tavernkeep.Infrastructure.Data.Context;
using Tavernkeep.Infrastructure.Data.Repositories.Base;

namespace Tavernkeep.Infrastructure.Data.Repositories
{
	public class ConditionLibraryEFRepository(SessionContext context) : EntityFrameworkNameRepository<Condition>(context), IConditionLibraryRepository
	{
		public Task<List<Condition>> GetAllConditionsAsync(CancellationToken cancellationToken = default)
		{
			return AsQueryable().ToListAsync(cancellationToken);
		}

		public Task<Condition> GetConditionAsync(string name, CancellationToken cancellationToken = default)
		{
			return AsQueryable().Where(x => x.Name == name).FirstAsync(cancellationToken);
		}
	}
}
