using MediatR;
using Tavernkeep.Core.Entities;

namespace Tavernkeep.Application.UseCases.Conditions.Commands.RemoveCondition
{
	public class RemoveConditionCommand : IRequest<Character>
	{
		public Guid InitiatorId { get; set; }
		public Guid CharacterId { get; set; }
		public string ConditionName { get; set; }

		public RemoveConditionCommand(Guid initiatorId, Guid characterId, string conditionName)
		{
			InitiatorId = initiatorId;
			CharacterId = characterId;
			ConditionName = conditionName;
		}
	}
}
