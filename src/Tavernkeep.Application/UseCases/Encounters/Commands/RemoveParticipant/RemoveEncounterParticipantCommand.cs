using MediatR;

namespace Tavernkeep.Application.UseCases.Encounters.Commands.RemoveParticipant
{
	public class RemoveEncounterParticipantCommand(Guid initiatorId) : IRequest
	{
		public Guid InitiatorId { get; set; } = initiatorId;
	}
}
