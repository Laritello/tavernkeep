using MediatR;

namespace Tavernkeep.Application.UseCases.Encounters.Commands.EditEncounterStatus
{
	public class EditEncounterStatusCommand(Guid initiatorId) : IRequest
	{
		public Guid InitiatorId { get; set; } = initiatorId;
	}
}
