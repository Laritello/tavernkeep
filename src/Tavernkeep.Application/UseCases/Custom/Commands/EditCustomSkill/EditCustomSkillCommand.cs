using MediatR;

namespace Tavernkeep.Application.UseCases.Custom.Commands.EditCustomSkill
{
	public class EditCustomSkillCommand(Guid initiatorId, Guid characterId, string oldName, string newName) : IRequest
	{
		public Guid InitiatorId { get; set; } = initiatorId;
		public Guid CharacterId { get; set; } = characterId;
		public string OldName { get; set; } = oldName;
		public string NewName { get; set; } = newName;
	}
}
