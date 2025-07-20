using Tavernkeep.Domain.Entities;

namespace Tavernkeep.Domain.Specifications.Users;

public class UserFullSpecification : Specification<User>
{
	public UserFullSpecification(Guid id) : base(x => x.Id == id)
	{
		AddInclude(x => x.Characters);
		AddInclude(x => x.ActiveCharacter!);
	}
}
