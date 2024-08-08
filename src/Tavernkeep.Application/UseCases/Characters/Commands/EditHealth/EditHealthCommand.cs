using MediatR;
using Tavernkeep.Core.Entities;

namespace Tavernkeep.Application.UseCases.Characters.Commands.EditHealth
{
	public class EditHealthCommand : IRequest<Health>
	{
		public Guid InitiatorId { get; set; }
		public Guid CharacterId { get; set; }
		public int Current { get; set; }
		public int Max { get; set; }
		public int Temporary { get; set; }

		public EditHealthCommand(Guid initiatorId, Guid characterId, int current, int max, int temporary)
		{
			InitiatorId = initiatorId;
			CharacterId = characterId;
			Current = current;
			Max = max;
			Temporary = temporary;
		}
	}
}
