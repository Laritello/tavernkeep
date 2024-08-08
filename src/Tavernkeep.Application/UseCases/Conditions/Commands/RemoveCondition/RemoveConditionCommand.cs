using MediatR;
using Tavernkeep.Core.Entities.Pathfinder;

namespace Tavernkeep.Application.UseCases.Conditions.Commands.RemoveCondition
{
	public class RemoveConditionCommand(Guid initiatorId, Guid characterId, string conditionName) : IRequest<Character>
	{
		public Guid InitiatorId { get; set; } = initiatorId;
		public Guid CharacterId { get; set; } = characterId;
		public string ConditionName { get; set; } = conditionName;
	}
}
