using MediatR;
using Tavernkeep.Core.Entities.Encounters;
using Tavernkeep.Core.Services;

namespace Tavernkeep.Application.UseCases.Encounters.Queries.GetAllEncounters
{
	public class GetAllEncounterQueryHandler(
		IEncounterService encounterService
		) : IRequestHandler<GetAllEncounterQuery, Dictionary<Guid, Encounter>>
	{
		public async Task<Dictionary<Guid, Encounter>> Handle(GetAllEncounterQuery request, CancellationToken cancellationToken)
		{
			var encounters = await encounterService.GetAllEncountersAsync(cancellationToken);
			return encounters.ToDictionary(x => x.Id);
		}
	}
}
