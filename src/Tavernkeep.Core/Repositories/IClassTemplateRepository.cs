using Tavernkeep.Core.Entities.Templates;

namespace Tavernkeep.Core.Repositories
{
	public interface IClassTemplateRepository : IStringRepositoryBase<ClassTemplate, string> 
	{
		Task<List<ClassTemplate>> GetAllClassesAsync(CancellationToken cancellationToken = default);
	}
}
