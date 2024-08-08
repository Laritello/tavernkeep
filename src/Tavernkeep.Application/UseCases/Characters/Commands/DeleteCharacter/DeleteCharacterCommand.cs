using MediatR;

namespace Tavernkeep.Application.UseCases.Characters.Commands.DeleteCharacter
{
	public class DeleteCharacterCommand(Guid initiatorId, Guid characterId) : IRequest
	{
		public Guid InitiatorId { get; set; } = initiatorId;
		public Guid CharacterId { get; set; } = characterId;
	}
}
