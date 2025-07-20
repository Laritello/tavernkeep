using Tavernkeep.Domain.Entities.Pathfinder;

namespace Tavernkeep.Domain.Services;

public interface IPortaitService
{
	Task<Portrait?> GetPortraitAsync(Guid characterId, CancellationToken cancellationToken);
	Task UpdatePortraitAsync(Guid characterId, byte[] bytes, string mimeType, CancellationToken cancellationToken);
}
