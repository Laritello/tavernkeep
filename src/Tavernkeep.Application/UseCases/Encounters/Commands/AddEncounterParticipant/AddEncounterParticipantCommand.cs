using MediatR;
using Tavernkeep.Core.Contracts.Enums;

namespace Tavernkeep.Application.UseCases.Encounters.Commands.AddEncounterParticipant
{
	public class AddEncounterParticipantCommand(Guid encounterId, EncounterParticipantType type, Guid characterId) : IRequest
	{
		public Guid EncounterId { get; set; } = encounterId;
		public EncounterParticipantType Type { get; set; } = type;
		public Guid ParticipantId { get; set; } = characterId;
	}
}
