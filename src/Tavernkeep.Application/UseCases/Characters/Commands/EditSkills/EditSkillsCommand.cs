using MediatR;
using Tavernkeep.Core.Contracts.Character.Dtos;

namespace Tavernkeep.Application.UseCases.Characters.Commands.EditSkills
{
	public class EditSkillsCommand(Guid initiatorId, Guid characterId, Dictionary<string, SkillEditDto> skills) : IRequest
	{
		public Guid InitiatorId { get; set; } = initiatorId;
		public Guid CharacterId { get; set; } = characterId;
		public Dictionary<string, SkillEditDto> Skills { get; set; } = skills;
	}
}
