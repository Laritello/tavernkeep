using Microsoft.EntityFrameworkCore;
using Tavernkeep.Domain.Entities.Encounters;
using Tavernkeep.Domain.Entities.Encounters.Participants;
using Tavernkeep.Domain.Repositories;
using Tavernkeep.Infrastructure.Data.Context;
using Tavernkeep.Infrastructure.Data.Repositories.Base;

namespace Tavernkeep.Infrastructure.Data.Repositories;

public class EncounterEFRepository(SessionContext context) : EntityFrameworkGuidRepository<Encounter>(context), IEncounterRepository
{
	public async Task<ICollection<Encounter>> GetAllEncountersAsync(CancellationToken cancellationToken = default)
	{
		return await AsQueryable()
			.Include(x => x.Participants)
			.ThenInclude(x => ((CreatureEncounterParticipant)x).Conditions)
			.ThenInclude(x => x.Condition)
			.Include(x => x.Participants)
			.ThenInclude(x => ((CreatureEncounterParticipant)x).Conditions)
			.ThenInclude(x => x.Creature)
			.ToListAsync(cancellationToken);
	}

	public async Task<Encounter?> GetFullEncounterAsync(Guid id, CancellationToken cancellationToken = default)
	{
		return await AsQueryable()
			.Where(x => x.Id == id)
			.Include(x => x.Participants)
			.ThenInclude(x => ((CreatureEncounterParticipant)x).Conditions)
			.ThenInclude(x => x.Condition)
			.Include(x => x.Participants)
			.ThenInclude(x => ((CreatureEncounterParticipant)x).Conditions)
			.ThenInclude(x => x.Creature)
			.FirstOrDefaultAsync(cancellationToken);
	}
}
