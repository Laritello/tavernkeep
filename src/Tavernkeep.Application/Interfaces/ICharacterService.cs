using Tavernkeep.Core.Entities;
using Tavernkeep.Core.Entities.Pathfinder;
using Tavernkeep.Core.Entities.Pathfinder.Builds;

namespace Tavernkeep.Application.Interfaces
{
	public interface ICharacterService
	{
		public Task<Character> CreateCharacterAsync(User owner, string name, string ancestryId, string backgroundId, string classId, CancellationToken cancellationToken);
		public Task<BuildDetails> GetBuild(Guid characterId, CancellationToken cancellationToken);
		public Task<Character> ApplyBuild(Guid characterId, BuildDetails details, CancellationToken cancellationToken);
		public Task<Character> GetCharacterAsync(Guid id, CancellationToken cancellationToken);
		public Task<List<Character>> GetAllCharactersAsync(CancellationToken cancellationToken);
	}
}
