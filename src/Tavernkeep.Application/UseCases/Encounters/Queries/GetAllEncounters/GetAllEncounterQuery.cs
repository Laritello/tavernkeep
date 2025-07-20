using MediatR;
using Tavernkeep.Domain.Entities.Encounters;

namespace Tavernkeep.Application.UseCases.Encounters.Queries.GetAllEncounters;

public class GetAllEncounterQuery : IRequest<Dictionary<Guid, Encounter>>
{
}
