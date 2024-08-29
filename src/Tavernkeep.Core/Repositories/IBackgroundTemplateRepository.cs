using Tavernkeep.Core.Entities.Templates;

namespace Tavernkeep.Core.Repositories
{
	public interface IBackgroundTemplateRepository : IStringRepositoryBase<BackgroundTemplate, string>
	{
		Task<List<BackgroundTemplate>> GetAllBackgroundsAsync(CancellationToken cancellationToken = default);
	}
}
