using MediatR;
using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Entities.Messages;

namespace Tavernkeep.Application.UseCases.Roll.Commands.RollSkill
{
	public class RollSkillCommand(Guid initiatorId, Guid characterId, string skillType, RollType rollType) : IRequest<SkillRollMessage>
	{
		public Guid InitiatorId { get; set; } = initiatorId;
		public Guid CharacterId { get; set; } = characterId;
		public string SkillType { get; set; } = skillType;
		public RollType RollType { get; set; } = rollType;
	}
}
