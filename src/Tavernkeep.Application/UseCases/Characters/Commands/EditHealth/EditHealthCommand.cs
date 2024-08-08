using MediatR;
using Tavernkeep.Core.Entities.Pathfinder;

namespace Tavernkeep.Application.UseCases.Characters.Commands.EditHealth
{
	public class EditHealthCommand(Guid initiatorId, Guid characterId, int current, int max, int temporary) : IRequest<Health>
	{
		public Guid InitiatorId { get; set; } = initiatorId;
		public Guid CharacterId { get; set; } = characterId;
		public int Current { get; set; } = current;
		public int Max { get; set; } = max;
		public int Temporary { get; set; } = temporary;
	}
}
