using Tavernkeep.Core.Entities.Pathfinder;
using Tavernkeep.Core.Repositories;
using Tavernkeep.Infrastructure.Data.Context;
using Tavernkeep.Infrastructure.Data.Repositories.Base;

namespace Tavernkeep.Infrastructure.Data.Repositories
{
	public class PortraitEFRepository(SessionContext context) : EntityFrameworkGuidRepository<Portrait>(context), IPortraitRepository
	{
	}
}
