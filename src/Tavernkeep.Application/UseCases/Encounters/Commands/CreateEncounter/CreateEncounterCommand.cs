using MediatR;
using Tavernkeep.Domain.Entities.Encounters;

namespace Tavernkeep.Application.UseCases.Encounters.Commands.CreateEncounter;

public class CreateEncounterCommand(string name) : IRequest<Encounter>
{
	public string Name { get; set; } = name;
}
