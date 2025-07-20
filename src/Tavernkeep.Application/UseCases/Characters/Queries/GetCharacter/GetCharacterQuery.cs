using MediatR;
using Tavernkeep.Domain.Entities.Pathfinder;

namespace Tavernkeep.Application.UseCases.Characters.Queries.GetCharacter;

public class GetCharacterQuery(Guid id) : IRequest<Character>
{
	public Guid Id { get; set; } = id;
}
