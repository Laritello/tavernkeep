using MediatR;
using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Entities.Pathfinder.Properties;

namespace Tavernkeep.Application.UseCases.Characters.Commands.EditPerception
{
	public class EditPerceptionCommand(Guid initiatorId, Guid characterId, Proficiency proficiency) : IRequest<Skill>
	{
		public Guid InitiatorId { get; set; } = initiatorId;
		public Guid CharacterId { get; set; } = characterId;
		public Proficiency Proficiency { get; set; } = proficiency;
	}
}
