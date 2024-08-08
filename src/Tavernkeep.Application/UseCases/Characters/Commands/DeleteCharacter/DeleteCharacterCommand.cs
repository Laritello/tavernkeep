using MediatR;

namespace Tavernkeep.Application.UseCases.Characters.Commands.DeleteCharacter
{
	public class DeleteCharacterCommand : IRequest
	{
		public Guid InitiatorId { get; set; }
		public Guid CharacterId { get; set; }

		public DeleteCharacterCommand(Guid initiatorId, Guid characterId)
		{
			InitiatorId = initiatorId;
			CharacterId = characterId;
		}
	}
}
