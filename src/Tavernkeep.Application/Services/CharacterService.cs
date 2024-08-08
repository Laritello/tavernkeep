using Tavernkeep.Application.Interfaces;
using Tavernkeep.Core.Entities;
using Tavernkeep.Core.Repositories;

namespace Tavernkeep.Application.Services
{
	public class CharacterService(ICharacterRepository characterRepository) : ICharacterService
	{
		public async Task<Character> CreateCharacterAsync(User owner, string name, CancellationToken cancellationToken = default)
		{
			Character character = new()
			{
				Owner = owner,
				Name = name,
			};

			characterRepository.Save(character);
			await characterRepository.CommitAsync(cancellationToken);

			return character;
		}
	}
}
