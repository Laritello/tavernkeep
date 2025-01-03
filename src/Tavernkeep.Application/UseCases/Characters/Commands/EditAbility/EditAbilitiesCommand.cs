using MediatR;
using Tavernkeep.Core.Contracts.Enums;

namespace Tavernkeep.Application.UseCases.Characters.Commands.EditAbility
{
	public class EditAbilitiesCommand(Guid initiatorId, Guid characterId, Dictionary<AbilityType, int> scores) : IRequest
	{
		public Guid InitiatorId { get; set; } = initiatorId;
		public Guid CharacterId { get; set; } = characterId;
		public Dictionary<AbilityType, int> Scores { get; set; } = scores;
	}
}
