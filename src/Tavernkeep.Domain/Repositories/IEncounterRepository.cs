using Tavernkeep.Domain.Entities.Encounters;

namespace Tavernkeep.Domain.Repositories
{
	public interface IEncounterRepository : IGuidRepositoryBase<Encounter, Guid>
	{
		public Task<ICollection<Encounter>> GetAllEncountersAsync(CancellationToken cancellationToken = default);
		public Task<Encounter?> GetFullEncounterAsync(Guid id, CancellationToken cancellationToken = default);
	}
}
