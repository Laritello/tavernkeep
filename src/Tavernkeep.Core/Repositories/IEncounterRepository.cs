using Tavernkeep.Core.Entities.Encounters;

namespace Tavernkeep.Core.Repositories
{
	public interface IEncounterRepository : IGuidRepositoryBase<Encounter, Guid>
	{
		public Task<ICollection<Encounter>> GetAllEncountersAsync(CancellationToken cancellationToken = default);
	}
}
