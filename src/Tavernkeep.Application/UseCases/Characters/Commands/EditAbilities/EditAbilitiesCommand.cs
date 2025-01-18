using MediatR;

namespace Tavernkeep.Application.UseCases.Characters.Commands.EditAbilities
{
	public class EditAbilitiesCommand(Guid initiatorId, Guid characterId, Dictionary<string, int> scores) : IRequest
	{
		public Guid InitiatorId { get; set; } = initiatorId;
		public Guid CharacterId { get; set; } = characterId;
		public Dictionary<string, int> Scores { get; set; } = scores;
	}
}
