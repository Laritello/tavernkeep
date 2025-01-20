using MediatR;
using Tavernkeep.Core.Contracts.Enums;

namespace Tavernkeep.Application.UseCases.Custom.Commands.AddCustomSkill
{
	public class AddCustomSkillCommand(Guid initiatorId, Guid characterId, string name, string? baseAbility, SkillType type) : IRequest
	{
		public Guid InitiatorId { get; set; } = initiatorId;
		public Guid CharacterId { get; set; } = characterId;
		public string Name { get; set; } = name;
		public string? BaseAbility { get; set; } = baseAbility;
		public SkillType Type { get; set; } = type;
	}
}
