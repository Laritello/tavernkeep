using MediatR;
using Tavernkeep.Core.Entities.Encounters;
using Tavernkeep.Core.Services;

namespace Tavernkeep.Application.UseCases.Encounters.Queries.GetEncounter
{
	public class GetEncounterQueryHandler(
		IEncounterService encounterService
		) : IRequestHandler<GetEncounterQuery, Encounter>
	{
		public async Task<Encounter> Handle(GetEncounterQuery request, CancellationToken cancellationToken)
		{
			return await encounterService.GetEncounterAsync(request.EncounterId, cancellationToken);
		}
	}
}
