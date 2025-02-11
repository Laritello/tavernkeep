using MediatR;

namespace Tavernkeep.Application.UseCases.Encounters.Commands.RemoveEncounterParticipant
{
	public class RemoveEncounterParticipantCommand(Guid encounterId, Guid participantId) : IRequest
	{
		public Guid EncounterId { get; set; } = encounterId;
		public Guid ParticipantId { get; set; } = participantId;
	}
}
