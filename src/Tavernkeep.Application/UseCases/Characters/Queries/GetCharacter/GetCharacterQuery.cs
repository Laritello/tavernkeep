using MediatR;
using Tavernkeep.Core.Entities.Pathfinder;

namespace Tavernkeep.Application.UseCases.Characters.Queries.GetCharacter
{
	public class GetCharacterQuery(Guid id) : IRequest<Character>
	{
		public Guid Id { get; set; } = id;
	}
}
