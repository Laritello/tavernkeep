using MediatR;
using Tavernkeep.Core.Entities.Encounters;

namespace Tavernkeep.Application.UseCases.Encounters.Commands.CreateEncounter
{
	public class CreateEncounterCommand(Guid initiatorId, string name) : IRequest<Encounter>
	{
		public Guid InitiatorId { get; set; } = initiatorId;
		public string Name { get; set; } = name;
	}
}
