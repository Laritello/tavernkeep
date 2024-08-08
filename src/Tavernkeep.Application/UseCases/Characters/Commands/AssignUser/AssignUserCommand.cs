using MediatR;
using Tavernkeep.Core.Entities.Pathfinder;

namespace Tavernkeep.Application.UseCases.Characters.Commands.AssignUser
{
	public class AssignUserCommand(Guid characterId, Guid userId) : IRequest<Character>
	{
		public Guid CharacterId { get; set; } = characterId;
		public Guid UserId { get; set; } = userId;
	}
}
