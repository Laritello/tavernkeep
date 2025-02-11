using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Entities.Encounters;

namespace Tavernkeep.Core.Services
{
	public interface IEncounterService
	{
		public Task<ICollection<Encounter>> GetAllEncountersAsync(CancellationToken cancellationToken);
		public Task<Encounter> GetEncounterAsync(Guid encounterId, CancellationToken cancellationToken);
		public Task<Encounter> CreateEncounterAsync(string name, CancellationToken cancellationToken);
		public Task UpdateEncounterStatusAsync(Guid encounterId, EncounterStatus status, CancellationToken cancellationToken);
	}
}
