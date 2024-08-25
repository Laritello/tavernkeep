using Microsoft.EntityFrameworkCore;
using Tavernkeep.Core.Entities.Pathfinder.Classes;
using Tavernkeep.Core.Repositories;
using Tavernkeep.Infrastructure.Data.Context;
using Tavernkeep.Infrastructure.Data.Repositories.Base;

namespace Tavernkeep.Infrastructure.Data.Repositories
{
	public class ClassMetadataEFRepository(SessionContext context) : EntityFrameworkNameRepository<ClassMetadata>(context), IClassMetadataRepository 
	{
		public Task<List<ClassMetadata>> GetAllClassesAsync(CancellationToken cancellationToken = default)
		{
			return AsQueryable().ToListAsync(cancellationToken);
		}
	}
}
