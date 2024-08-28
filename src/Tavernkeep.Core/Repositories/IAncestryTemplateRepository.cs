using Tavernkeep.Core.Entities.Templates;

namespace Tavernkeep.Core.Repositories
{
	public interface IAncestryTemplateRepository : IStringRepositoryBase<AncestryTemplate, string> 
	{
		Task<List<AncestryTemplate>> GetAllAncestriesAsync(CancellationToken cancellationToken = default);
	}
}
