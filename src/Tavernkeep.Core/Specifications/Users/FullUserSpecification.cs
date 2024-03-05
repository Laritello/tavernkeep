using Tavernkeep.Core.Entities;

namespace Tavernkeep.Core.Specifications.Users
{
    public class FullUserSpecification : Specification<User>
    {
        public FullUserSpecification(Guid id) : base(x => x.Id == id)
        {
            AddInclude(x => x.Characters);
            AddInclude(x => x.ActiveCharacter);
        }
    }
}
