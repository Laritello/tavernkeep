using Microsoft.EntityFrameworkCore;
using Tavernkeep.Core.Entities.Templates;
using Tavernkeep.Core.Repositories;
using Tavernkeep.Infrastructure.Data.Context;
using Tavernkeep.Infrastructure.Data.Repositories.Base;

namespace Tavernkeep.Infrastructure.Data.Repositories
{
	public class AncestryTemplateEFRepository(SessionContext context) : EntityFrameworkNameRepository<AncestryTemplate>(context), IAncestryTemplateRepository
	{
		public async Task<List<AncestryTemplate>> GetAllAncestriesAsync(CancellationToken cancellationToken = default)
		{
			return await AsQueryable().ToListAsync(cancellationToken);
		}
	}
}
