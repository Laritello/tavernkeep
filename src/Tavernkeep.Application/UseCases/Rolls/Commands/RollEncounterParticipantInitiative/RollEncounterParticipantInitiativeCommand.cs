using MediatR;

namespace Tavernkeep.Application.UseCases.Rolls.Commands.RollEncounterParticipantInitiative
{
	public class RollEncounterParticipantInitiativeCommand(Guid initiatorId, Guid encounterId, Guid characterId, string initiativeSkill) : IRequest
	{
		public Guid InitiatorId { get; set; } = initiatorId;
		public Guid EncounterId { get; set; } = encounterId;
		public Guid ParticipantId { get; set; } = characterId;
		public string InitiativeSkill { get; set; } = initiativeSkill;
	}
}
