using Tavernkeep.Core.Entities.Pathfinder.Ancestries;
using Tavernkeep.Core.Repositories;
using Tavernkeep.Infrastructure.Data.Context;
using Tavernkeep.Infrastructure.Data.Repositories.Base;

namespace Tavernkeep.Infrastructure.Data.Repositories
{
	public class AncestryMetadataEFRepository(SessionContext context) : EntityFrameworkNameRepository<AncestryMetadata>(context), IAncestryMetadataRepository { }
}
