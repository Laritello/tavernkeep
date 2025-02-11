using MediatR;
using Tavernkeep.Core.Entities.Encounters;

namespace Tavernkeep.Application.UseCases.Encounters.Queries.GetAllEncounters
{
	public class GetAllEncounterQuery : IRequest<Dictionary<Guid, Encounter>>
	{
	}
}
