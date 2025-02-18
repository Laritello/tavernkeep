using Microsoft.EntityFrameworkCore;
using Tavernkeep.Core.Entities.Pathfinder.Conditions;
using Tavernkeep.Core.Repositories;
using Tavernkeep.Infrastructure.Data.Context;
using Tavernkeep.Infrastructure.Data.Repositories.Base;

namespace Tavernkeep.Infrastructure.Data.Repositories
{
	public class ConditionLibraryEFRepository(SessionContext context) : EntityFrameworkNameRepository<ConditionInformation>(context), IConditionLibraryRepository
	{
		public Task<List<ConditionInformation>> GetAllConditionsAsync(CancellationToken cancellationToken = default)
		{
			return AsQueryable().ToListAsync(cancellationToken);
		}

		public Task<ConditionInformation> GetConditionAsync(string name, CancellationToken cancellationToken = default)
		{
			// TODO: Fix exceptions
			return AsQueryable().Where(x => x.Name == name).FirstAsync(cancellationToken);
		}
	}
}
