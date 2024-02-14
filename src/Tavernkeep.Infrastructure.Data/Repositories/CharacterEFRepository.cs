using Microsoft.EntityFrameworkCore;
using Tavernkeep.Core.Entities;
using Tavernkeep.Core.Repositories;
using Tavernkeep.Infrastructure.Data.Context;

namespace Tavernkeep.Infrastructure.Data.Repositories
{
    public class CharacterEFRepository(SessionContext context) : EntityFrameworkRepository<Character>(context), ICharacterRepository
    {
        public async Task<Character?> GetFullCharacter(Guid id, CancellationToken cancellationToken = default)
        {
            return await AsQueryable().Where(x => x.Id == id).Include(x => x.Owner).FirstOrDefaultAsync(cancellationToken);
        }
    }
}
