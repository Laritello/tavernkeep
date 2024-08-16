using Tavernkeep.Core.Entities.Pathfinder.Properties;
using Tavernkeep.Core.Repositories;
using Tavernkeep.Infrastructure.Data.Context;
using Tavernkeep.Infrastructure.Data.Repositories.Base;

namespace Tavernkeep.Infrastructure.Data.Repositories
{
	public class AncestryEFRepository(SessionContext context) : EntityFrameworkNameRepository<Ancestry>(context), IAncestryRepository { }
}
