using MediatR;
using Tavernkeep.Core.Contracts.Enums;

namespace Tavernkeep.Application.UseCases.Characters.Commands.EditPerception
{
	public class EditPerceptionCommand(Guid initiatorId, Guid characterId, Proficiency proficiency) : IRequest
	{
		public Guid InitiatorId { get; set; } = initiatorId;
		public Guid CharacterId { get; set; } = characterId;
		public Proficiency Proficiency { get; set; } = proficiency;
	}
}
