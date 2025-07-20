using MediatR;

namespace Tavernkeep.Application.UseCases.Encounters.Commands.DeleteConditionFromParticipant;

public class DeleteConditionFromParticipantCommand(Guid encounterId, Guid participantId, string name) : IRequest
{
	public Guid EncounterId { get; set; } = encounterId;
	public Guid ParticipantId { get; set; } = participantId;
	public string Name { get; set; } = name;
}
