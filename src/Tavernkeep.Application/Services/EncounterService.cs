using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Entities.Encounters;
using Tavernkeep.Core.Exceptions;
using Tavernkeep.Core.Repositories;
using Tavernkeep.Core.Services;

namespace Tavernkeep.Application.Services
{
	public class EncounterService(
		IEncounterRepository encounterRepository
		) : IEncounterService
	{
		public async Task<Encounter> CreateEncounterAsync(string name, CancellationToken cancellationToken)
		{
			Encounter encounter = new(name, EncounterStatus.Draft);

			encounterRepository.Save(encounter);
			await encounterRepository.CommitAsync(cancellationToken);

			return encounter;
		}

		public async Task UpdateEncounterStatusAsync(Guid encounterId, EncounterStatus status, CancellationToken cancellationToken)
		{
			var encounter = await encounterRepository.FindAsync(encounterId, cancellationToken: cancellationToken)
				?? throw new BusinessLogicException("Encounter not found.");

			encounter.Status = status;

			encounterRepository.Save(encounter);
			await encounterRepository.CommitAsync(cancellationToken);
		}
	}
}
