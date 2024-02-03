using Tavernkeep.Core.Entities;
using Tavernkeep.Core.Repositories;
using Tavernkeep.Infrastructure.Context;

namespace Tavernkeep.Infrastructure.Repositories
{
    public class CharacterEFRepository(SessionContext context) : EntityFrameworkRepository<Character>(context), ICharacterRepository
    {

    }
}
