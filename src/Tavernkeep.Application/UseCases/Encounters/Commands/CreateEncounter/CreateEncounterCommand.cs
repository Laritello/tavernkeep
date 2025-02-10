using MediatR;

namespace Tavernkeep.Application.UseCases.Encounters.Commands.CreateEncounter
{
	public class CreateEncounterCommand(Guid initiatorId) : IRequest
	{
		public Guid InitiatorId { get; set; } = initiatorId;
	}
}
