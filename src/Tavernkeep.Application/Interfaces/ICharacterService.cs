using Tavernkeep.Core.Entities;
using Tavernkeep.Core.Entities.Pathfinder;

namespace Tavernkeep.Application.Interfaces
{
	public interface ICharacterService
	{
		public Task<Character> CreateCharacterAsync(User owner, string name, CancellationToken cancellationToken);
		public Task<Character> GetCharacter(Guid id, CancellationToken cancellationToken);
	}
}
