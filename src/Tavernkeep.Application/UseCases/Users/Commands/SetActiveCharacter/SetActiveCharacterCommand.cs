using MediatR;
using Tavernkeep.Core.Entities;

namespace Tavernkeep.Application.UseCases.Users.Commands.SetActiveCharacter
{
	public class SetActiveCharacterCommand(Guid initiatorId, Guid userId, Guid characterId) : IRequest<User>
	{
		public Guid InitiatorId { get; set; } = initiatorId;
		public Guid UserId { get; set; } = userId;
		public Guid CharacterId { get; set; } = characterId;
	}
}
