using Tavernkeep.Domain.Entities.Pathfinder;
using Tavernkeep.Domain.Repositories;
using Tavernkeep.Infrastructure.Data.Context;
using Tavernkeep.Infrastructure.Data.Repositories.Base;

namespace Tavernkeep.Infrastructure.Data.Repositories;

public class PortraitEFRepository(SessionContext context) : EntityFrameworkGuidRepository<Portrait>(context), IPortraitRepository
{
}
