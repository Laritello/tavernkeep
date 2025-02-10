using Tavernkeep.Core.Entities.Pathfinder;

namespace Tavernkeep.Core.Specifications.Characters
{
	public class CharacterPortraitSpecification : Specification<Character>
	{
		public CharacterPortraitSpecification(Guid id) : base(x => x.Id == id)
		{
			AddInclude(x => x.Portrait!);
		}
	}
}
