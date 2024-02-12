using Tavernkeep.Core.Entities;
using Tavernkeep.Core.Repositories;
using Tavernkeep.Infrastructure.Data.Context;

namespace Tavernkeep.Infrastructure.Data.Repositories
{
    public class CharacterEFRepository(SessionContext context) : EntityFrameworkRepository<Character>(context), ICharacterRepository
    {

    }
}
