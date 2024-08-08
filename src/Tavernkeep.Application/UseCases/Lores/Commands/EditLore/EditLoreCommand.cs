using MediatR;
using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Entities.Pathfinder;

namespace Tavernkeep.Application.UseCases.Lores.Commands.EditLore
{
	public class EditLoreCommand(Guid initiatorId, Guid characterId, string topic, Proficiency proficiency) : IRequest<Lore>
	{
		public Guid InitiatorId { get; set; } = initiatorId;
		public Guid CharacterId { get; set; } = characterId;
		public string Topic { get; set; } = topic;
		public Proficiency Proficiency { get; set; } = proficiency;
	}
}
