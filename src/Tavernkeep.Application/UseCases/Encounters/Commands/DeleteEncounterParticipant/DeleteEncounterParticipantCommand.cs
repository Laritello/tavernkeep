using MediatR;

namespace Tavernkeep.Application.UseCases.Encounters.Commands.DeleteEncounterParticipant;

public class DeleteEncounterParticipantCommand(Guid encounterId, Guid participantId) : IRequest
{
	public Guid EncounterId { get; set; } = encounterId;
	public Guid ParticipantId { get; set; } = participantId;
}
