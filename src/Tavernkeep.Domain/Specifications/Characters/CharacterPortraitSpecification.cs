using Tavernkeep.Domain.Entities.Pathfinder;

namespace Tavernkeep.Domain.Specifications.Characters;

public class CharacterPortraitSpecification : Specification<Character>
{
	public CharacterPortraitSpecification(Guid id) : base(x => x.Id == id)
	{
		AddInclude(x => x.Portrait!);
	}
}
