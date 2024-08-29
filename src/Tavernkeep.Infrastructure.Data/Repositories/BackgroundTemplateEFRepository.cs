using Microsoft.EntityFrameworkCore;
using Tavernkeep.Core.Entities.Templates;
using Tavernkeep.Core.Repositories;
using Tavernkeep.Infrastructure.Data.Context;
using Tavernkeep.Infrastructure.Data.Repositories.Base;

namespace Tavernkeep.Infrastructure.Data.Repositories
{
	public class BackgroundTemplateEFRepository(SessionContext context) : EntityFrameworkNameRepository<BackgroundTemplate>(context), IBackgroundTemplateRepository
	{
		public async Task<List<BackgroundTemplate>> GetAllBackgroundsAsync(CancellationToken cancellationToken = default)
		{
			return await AsQueryable().ToListAsync(cancellationToken);
		}
	}
}
