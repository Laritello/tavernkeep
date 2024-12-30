using Tavernkeep.Application.Interfaces;
using Tavernkeep.Core.Entities;
using Tavernkeep.Core.Entities.Pathfinder;
using Tavernkeep.Core.Exceptions;
using Tavernkeep.Core.Repositories;

namespace Tavernkeep.Application.Services
{
	// TODO: Optimize and refactor!!!
	public class CharacterService(ICharacterRepository characterRepository) : ICharacterService
	{
		public async Task<Character> CreateCharacterAsync(User owner, string name, string ancestryId, string backgroundId, string classId, CancellationToken cancellationToken = default)
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

		public async Task<List<Character>> GetAllCharactersAsync(CancellationToken cancellationToken)
		{
			return await characterRepository.GetAllCharactersAsync(cancellationToken);
		}

		public async Task<Character> GetCharacterAsync(Guid id, CancellationToken cancellationToken)
		{
			var character = await characterRepository.FindAsync(id, cancellationToken: cancellationToken)
				?? throw new BusinessLogicException("No character with provided ID found.");

			return character;
		}

	}
}
