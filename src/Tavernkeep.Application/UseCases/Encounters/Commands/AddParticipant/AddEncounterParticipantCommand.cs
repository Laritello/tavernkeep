using MediatR;

namespace Tavernkeep.Application.UseCases.Encounters.Commands.AddParticipant
{
	public class AddEncounterParticipantCommand(Guid initiatorId) : IRequest
	{
		public Guid InitiatorId { get; set; } = initiatorId;
	}
}
