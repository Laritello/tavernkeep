using Tavernkeep.Core.Entities.Pathfinder;

namespace Tavernkeep.Core.Services
{
	public interface IPortaitService
	{
		Task<Portrait> GetPortraitAsync(Guid characterId, CancellationToken cancellationToken);
		Task UpdatePortraitAsync(Guid characterId, byte[] bytes, string mimeType, CancellationToken cancellationToken);
	}
}
