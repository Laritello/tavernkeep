using MediatR;
using Tavernkeep.Domain.Entities.Encounters;

namespace Tavernkeep.Application.UseCases.Encounters.Queries.GetEncounter;

public class GetEncounterQuery(Guid encounterId) : IRequest<Encounter>
{
	public Guid EncounterId { get; set; } = encounterId;
}
