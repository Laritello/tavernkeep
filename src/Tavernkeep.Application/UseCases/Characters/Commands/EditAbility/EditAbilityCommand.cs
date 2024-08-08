using MediatR;
using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Entities.Pathfinder;

namespace Tavernkeep.Application.UseCases.Characters.Commands.EditAbility
{
	public class EditAbilityCommand(Guid initiatorId, Guid characterId, AbilityType type, int score) : IRequest<Ability>
	{
		public Guid InitiatorId { get; set; } = initiatorId;
		public Guid CharacterId { get; set; } = characterId;
		public AbilityType Type { get; set; } = type;
		public int Score { get; set; } = score;
	}
}
