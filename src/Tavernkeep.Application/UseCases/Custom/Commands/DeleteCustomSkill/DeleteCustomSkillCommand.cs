using MediatR;

namespace Tavernkeep.Application.UseCases.Custom.Commands.DeleteCustomSkill
{
	public class DeleteCustomSkillCommand(Guid initiatorId, Guid characterId, string name) : IRequest
	{
		public Guid InitiatorId { get; set; } = initiatorId;
		public Guid CharacterId { get; set; } = characterId;
		public string Name { get; set; } = name;
	}
}
