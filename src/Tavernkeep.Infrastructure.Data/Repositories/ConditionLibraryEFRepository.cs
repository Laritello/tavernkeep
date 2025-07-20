using Microsoft.EntityFrameworkCore;
using Tavernkeep.Domain.Entities.Library.Conditions;
using Tavernkeep.Domain.Repositories;
using Tavernkeep.Infrastructure.Data.Context;
using Tavernkeep.Infrastructure.Data.Repositories.Base;

namespace Tavernkeep.Infrastructure.Data.Repositories;

public class ConditionLibraryEFRepository(SessionContext context) : EntityFrameworkNameRepository<Condition>(context), IConditionLibraryRepository
{
	public Task<List<Condition>> GetAllConditionsAsync(CancellationToken cancellationToken = default)
	{
		return AsQueryable()
			.Include(x => x.Related).ThenInclude(x => x.Condition)
			.ToListAsync(cancellationToken);
	}

	public Task<Condition> GetConditionAsync(string name, CancellationToken cancellationToken = default)
	{
		return AsQueryable().Where(x => x.Name == name)
			.Include(x => x.Related).ThenInclude(x => x.Condition)
			.FirstAsync(cancellationToken);
	}
}
