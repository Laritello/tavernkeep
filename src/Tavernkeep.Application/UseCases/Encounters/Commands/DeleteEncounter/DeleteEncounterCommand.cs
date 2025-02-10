using MediatR;

namespace Tavernkeep.Application.UseCases.Encounters.Commands.DeleteEncounter
{
	public class DeleteEncounterCommand(Guid initiatorId) : IRequest
	{
		public Guid InitiatorId { get; set; } = initiatorId;
	}
}
