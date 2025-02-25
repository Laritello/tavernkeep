using MediatR;

namespace Tavernkeep.Application.UseCases.Encounters.Commands.EditConditionOnParticipant
{
	public class EditConditionOnParticipantCommand(Guid encounterId, Guid participantId, string name, int level) : IRequest
	{
		public Guid EncounterId { get; set; } = encounterId;
		public Guid ParticipantId { get; set; } = participantId;
		public string Name { get; set; } = name;
		public int Level { get; set; } = level;
	}
}
