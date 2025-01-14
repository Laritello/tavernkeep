using MediatR;

namespace Tavernkeep.Application.UseCases.Characters.Commands.EditHeroPoints
{
	public class EditHeroPointsCommand(Guid initiatorId, Guid characterId, int amount) : IRequest
	{
		public Guid InitiatorId { get; set; } = initiatorId;
		public Guid CharacterId { get; set; } = characterId;
		public int Amount { get; set; } = amount;
	}
}
