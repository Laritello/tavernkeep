using Microsoft.EntityFrameworkCore;
using Tavernkeep.Core.Entities.Templates;
using Tavernkeep.Core.Repositories;
using Tavernkeep.Infrastructure.Data.Context;
using Tavernkeep.Infrastructure.Data.Repositories.Base;

namespace Tavernkeep.Infrastructure.Data.Repositories
{
	public class ClassTemplateEFRepository(SessionContext context) : EntityFrameworkNameRepository<ClassTemplate>(context), IClassTemplateRepository 
	{
		public Task<List<ClassTemplate>> GetAllClassesAsync(CancellationToken cancellationToken = default)
		{
			return AsQueryable().ToListAsync(cancellationToken);
		}
	}
}
