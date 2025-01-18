using Microsoft.EntityFrameworkCore;
using Tavernkeep.Core.Entities.Pathfinder;
using Tavernkeep.Core.Repositories;
using Tavernkeep.Infrastructure.Data.Context;
using Tavernkeep.Infrastructure.Data.Repositories.Base;

namespace Tavernkeep.Infrastructure.Data.Repositories
{
	public class CharacterEFRepository(SessionContext context) : EntityFrameworkGuidRepository<Character>(context), ICharacterRepository
	{
		public async Task<List<Character>> GetAllCharactersAsync(CancellationToken cancellationToken = default)
		{
			return await AsQueryable()
				.Include(x => x.Owner)
				.Include(x => x.Abilities)
				.Include(x => x.Skills).ThenInclude(x => x.Ability)
				.Include(x => x.SavingThrows).ThenInclude(x => x.Ability)
				.ToListAsync(cancellationToken);
		}
		public async Task<Character?> GetFullCharacterAsync(Guid id, CancellationToken cancellationToken = default)
		{
			return await AsQueryable().Where(x => x.Id == id)
				.Include(x => x.Owner)
				.Include(x => x.Abilities)
				.Include(x => x.Skills).ThenInclude(x => x.Ability)
				.Include(x => x.SavingThrows).ThenInclude(x => x.Ability)
				.FirstOrDefaultAsync(cancellationToken);
		}
	}
}
