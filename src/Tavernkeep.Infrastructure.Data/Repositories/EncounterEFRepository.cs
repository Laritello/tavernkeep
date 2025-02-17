using Microsoft.EntityFrameworkCore;
using Tavernkeep.Core.Entities.Encounters;
using Tavernkeep.Core.Repositories;
using Tavernkeep.Infrastructure.Data.Context;
using Tavernkeep.Infrastructure.Data.Repositories.Base;

namespace Tavernkeep.Infrastructure.Data.Repositories
{
	public class EncounterEFRepository(SessionContext context) : EntityFrameworkGuidRepository<Encounter>(context), IEncounterRepository
	{
		public async Task<ICollection<Encounter>> GetAllEncountersAsync(CancellationToken cancellationToken = default)
		{
			return await AsQueryable().Include(x => x.Participants).ToListAsync(cancellationToken);
		}

		public async Task<Encounter?> GetFullEncounterAsync(Guid id, CancellationToken cancellationToken = default)
		{
			return await AsQueryable()
				.Where(x => x.Id == id)
				.Include(x => x.Participants)
				.FirstOrDefaultAsync(cancellationToken);
		}
	}
}
