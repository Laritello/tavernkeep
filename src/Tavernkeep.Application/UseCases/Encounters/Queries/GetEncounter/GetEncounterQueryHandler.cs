using MediatR;
using Tavernkeep.Domain.Entities.Encounters;
using Tavernkeep.Domain.Services;

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
