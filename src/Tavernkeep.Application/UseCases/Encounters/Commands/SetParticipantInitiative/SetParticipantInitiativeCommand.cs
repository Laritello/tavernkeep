using MediatR;

namespace Tavernkeep.Application.UseCases.Encounters.Commands.SetParticipantInitiative
{
	public class SetParticipantInitiativeCommand(Guid initiatorId, Guid encounterId, Guid participantId, int result) : IRequest
	{
		public Guid InitiatorId { get; set; } = initiatorId;
		public Guid EncounterId { get; set; } = encounterId;
		public Guid ParticipantId { get; set; } = participantId;
		public int Result { get; set; } = result;
	}
}
