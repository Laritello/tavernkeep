using MediatR;

namespace Tavernkeep.Application.UseCases.Characters.Commands.EditHealth
{
	public class EditHealthCommand(Guid initiatorId, Guid characterId, int current, int temporary) : IRequest
	{
		public Guid InitiatorId { get; set; } = initiatorId;
		public Guid CharacterId { get; set; } = characterId;
		public int Current { get; set; } = current;
		public int Temporary { get; set; } = temporary;
	}
}
