using Tavernkeep.Core.Entities.Encounters;
using Tavernkeep.Core.Repositories;
using Tavernkeep.Infrastructure.Data.Context;
using Tavernkeep.Infrastructure.Data.Repositories.Base;

namespace Tavernkeep.Infrastructure.Data.Repositories
{
	public class EncounterEFRepository(SessionContext context) : EntityFrameworkGuidRepository<Encounter>(context), IEncounterRepository
	{
	}
}
