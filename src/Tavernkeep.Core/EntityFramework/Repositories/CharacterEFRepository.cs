using Tavernkeep.Core.Entities;
using Tavernkeep.Core.EntityFramework.Context;
using Tavernkeep.Core.Repositories;

namespace Tavernkeep.Core.EntityFramework.Repositories
{
    public class CharacterEFRepository(ApplicationContext context) : EntityFrameworkRepository<Character>(context), ICharacterRepository
    {

    }
}
