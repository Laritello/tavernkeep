using Tavernkeep.Application.Interfaces;
using Tavernkeep.Core.Entities;
using Tavernkeep.Core.Entities.Pathfinder;
using Tavernkeep.Core.Entities.Pathfinder.Builds.Conversion.Converters;
using Tavernkeep.Core.Entities.Templates;
using Tavernkeep.Core.Exceptions;
using Tavernkeep.Core.Repositories;

namespace Tavernkeep.Application.Services
{
	public class CharacterService(
		ICharacterRepository characterRepository,
		IAncestryTemplateRepository ancestryRepository,
		IClassTemplateRepository classRepository
		) : ICharacterService
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

		public async Task<List<Character>> GetAllCharactersAsync(CancellationToken cancellationToken)
		{
			var characters = await characterRepository.GetAllCharactersAsync(cancellationToken);
			var ancestryTemplates = await ancestryRepository.GetAllAncestriesAsync(cancellationToken);
			var classTemplates = await classRepository.GetAllClassesAsync(cancellationToken);

			foreach (var character in characters)
			{
				RestoreCharacter(character, ancestryTemplates, classTemplates);
			}

			return characters;
		}

		public async Task<Character> GetCharacterAsync(Guid id, CancellationToken cancellationToken)
		{
			var character = await characterRepository.FindAsync(id, cancellationToken: cancellationToken)
				?? throw new BusinessLogicException("No character with provided ID found.");

			var ancestryTemplate = await ancestryRepository.FindAsync(character.Snapshot.Ancestry.Id, cancellationToken: cancellationToken)
				?? throw new BusinessLogicException("No ancestry with provided ID found.");

			var classTemplate = await classRepository.FindAsync(character.Snapshot.Class.Id, cancellationToken: cancellationToken)
				?? throw new BusinessLogicException("No class with provided ID found.");

			var generalConverter = new GeneralConverter(character);
			var ancestryConverter = new AncestryConverter(character, ancestryTemplate);
			var classConverter = new ClassConverter(character, ancestryTemplate, classTemplate);

			character.Build.General = generalConverter.Convert();
			character.Build.Ancestry = ancestryConverter.Convert();
			character.Build.Class = classConverter.Convert();

			return character;
		}

		private Character RestoreCharacter(Character character, List<AncestryTemplate> ancestryTemplates, List<ClassTemplate> classTemplates)
		{
			var ancestryTemplate = ancestryTemplates.FirstOrDefault(x => x.Id == character.Snapshot.Ancestry.Id)
				?? throw new BusinessLogicException("No ancestry with provided ID found.");

			var classTemplate = classTemplates.FirstOrDefault(x => x.Id == character.Snapshot.Class.Id)
				?? throw new BusinessLogicException("No class with provided ID found.");

			var generalConverter = new GeneralConverter(character);
			var ancestryConverter = new AncestryConverter(character, ancestryTemplate);
			var classConverter = new ClassConverter(character, ancestryTemplate, classTemplate);

			character.Build.General = generalConverter.Convert();
			character.Build.Ancestry = ancestryConverter.Convert();
			character.Build.Class = classConverter.Convert();

			return character;
		}
	}
}
