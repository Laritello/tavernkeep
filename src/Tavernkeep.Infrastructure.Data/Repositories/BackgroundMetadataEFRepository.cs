using Tavernkeep.Core.Entities.Pathfinder.Backgrounds;
using Tavernkeep.Core.Repositories;
using Tavernkeep.Infrastructure.Data.Context;
using Tavernkeep.Infrastructure.Data.Repositories.Base;

namespace Tavernkeep.Infrastructure.Data.Repositories
{
	public class BackgroundMetadataEFRepository(SessionContext context) : EntityFrameworkNameRepository<BackgroundMetadata>(context), IBackgroundMetadataRepository { }
}
