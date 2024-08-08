using Tavernkeep.Core.Entities;

namespace Tavernkeep.Application.Interfaces
{
	public interface ICharacterService
	{
		public Task<Character> CreateCharacterAsync(User owner, string name, CancellationToken cancellationToken);
	}
}
