using MediatR;
using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Entities;

namespace Tavernkeep.Application.UseCases.Characters.Commands.EditSkill
{
	public class EditSkillCommand(Guid initiatorId, Guid characterId, SkillType type, Proficiency proficiency) : IRequest<Skill>
	{
		public Guid InitiatorId { get; set; } = initiatorId;
		public Guid CharacterId { get; set; } = characterId;
		public SkillType Type { get; set; } = type;
		public Proficiency Proficiency { get; set; } = proficiency;
	}
}
