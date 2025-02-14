using MediatR;

namespace Tavernkeep.Application.UseCases.Rolls.Commands.RollEncounterInitiative
{
	public class RollEncounterInitiativeCommand(Guid initiatorId, Guid encounterId, bool rollNPConly) : IRequest
	{
		public Guid InitiatorId { get; set; } = initiatorId;
		public Guid EncounterId { get; set; } = encounterId;
		public bool NPCOnly { get; set; } = rollNPConly;
	}
}
