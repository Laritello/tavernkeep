using Microsoft.EntityFrameworkCore;
using Tavernkeep.Domain.Entities.Library.Creatures;
using Tavernkeep.Domain.Repositories;
using Tavernkeep.Infrastructure.Data.Context;
using Tavernkeep.Infrastructure.Data.Repositories.Base;

namespace Tavernkeep.Infrastructure.Data.Repositories
{
	public class CreatureLibraryEFRepository(SessionContext context) : EntityFrameworkGuidRepository<Creature>(context), ICreatureLibraryRepository
	{
		public Task<List<Creature>> GetAllCreaturesAsync(CancellationToken cancellationToken = default)
		{
			return AsQueryable().OrderBy(x => x.Level).ThenBy(x => x.Name).ToListAsync(cancellationToken);
		}

		public Task<Creature> GetCreatureAsync(Guid id, CancellationToken cancellationToken = default)
		{
			return AsQueryable().Where(x => x.Id == id).FirstAsync(cancellationToken);
		}
	}
}
