using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Entities.Encounters;

namespace Tavernkeep.Core.Services
{
	public interface IEncounterService
	{
		public Task<Encounter> CreateEncounterAsync(string name, CancellationToken cancellationToken);
		public Task UpdateEncounterStatusAsync(Guid encounterId, EncounterStatus status, CancellationToken cancellationToken);
	}
}
