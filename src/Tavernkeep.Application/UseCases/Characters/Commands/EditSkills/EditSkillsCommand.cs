using MediatR;
using Tavernkeep.Core.Contracts.Enums;

namespace Tavernkeep.Application.UseCases.Characters.Commands.EditSkills
{
	public class EditSkillsCommand(Guid initiatorId, Guid characterId, Dictionary<string, Proficiency> proficiencies) : IRequest
	{
		public Guid InitiatorId { get; set; } = initiatorId;
		public Guid CharacterId { get; set; } = characterId;
		public Dictionary<string, Proficiency> Proficiencies { get; set; } = proficiencies;
	}
}
