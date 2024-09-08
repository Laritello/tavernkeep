using Tavernkeep.Application.Interfaces;
using Tavernkeep.Core.Entities;
using Tavernkeep.Core.Entities.Pathfinder;
using Tavernkeep.Core.Entities.Pathfinder.Builds;
using Tavernkeep.Core.Entities.Pathfinder.Builds.Conversion.Converters;
using Tavernkeep.Core.Entities.Pathfinder.Builds.Parts;
using Tavernkeep.Core.Entities.Templates;
using Tavernkeep.Core.Exceptions;
using Tavernkeep.Core.Repositories;

namespace Tavernkeep.Application.Services
{
	// TODO: Optimize and refactor!!!
	public class CharacterService(
		ICharacterRepository characterRepository,
		IAncestryTemplateRepository ancestryRepository,
		IBackgroundTemplateRepository backgroundRepository,
		IClassTemplateRepository classRepository)
		: ICharacterService
	{
		public async Task<Character> CreateCharacterAsync(User owner, string name, string ancestryId, string backgroundId, string classId, CancellationToken cancellationToken = default)
		{
			Character character = new()
			{
				Owner = owner,
				Name = name,
			};
			var generalTemplate = new General();

			var ancestryTemplate = await ancestryRepository.FindAsync(ancestryId, cancellationToken: cancellationToken)
				?? throw new BusinessLogicException("No ancestry with provided ID found.");

			var backgroundTemplate = await backgroundRepository.FindAsync(backgroundId, cancellationToken: cancellationToken)
				?? throw new BusinessLogicException("No background with provided ID found.");

			var classTemplate = await classRepository.FindAsync(classId, cancellationToken: cancellationToken)
				?? throw new BusinessLogicException("No class with provided ID found.");

			var generalConverter = new GeneralConverter(character, generalTemplate);
			var ancestryConverter = new AncestryConverter(character, ancestryTemplate);
			var backgroundConverter = new BackgroundConverter(character, backgroundTemplate);
			var classConverter = new ClassConverter(character, ancestryTemplate, classTemplate);

			character.Build.Level = 1;
			character.Build.General = generalConverter.Convert();
			character.Build.Ancestry = ancestryConverter.Convert();
			character.Build.Background = backgroundConverter.Convert();
			character.Build.Class = classConverter.Convert();

			character.Snapshot.General = generalConverter.Snapshot();
			character.Snapshot.Ancestry = ancestryConverter.Snapshot();
			character.Snapshot.Background = backgroundConverter.Snapshot();
			character.Snapshot.Class = classConverter.Snapshot();

			characterRepository.Save(character);
			await characterRepository.CommitAsync(cancellationToken);

			return character;
		}

		public async Task<BuildDetails> GetBuild(Guid characterId, CancellationToken cancellationToken = default)
		{
			var character = await characterRepository.FindAsync(characterId, cancellationToken: cancellationToken)
				?? throw new BusinessLogicException("No character with provided ID found.");

			var generalTemplate = new General();

			var ancestryTemplate = await ancestryRepository.FindAsync(character.Snapshot.Ancestry.Id, cancellationToken: cancellationToken)
				?? throw new BusinessLogicException("No ancestry with provided ID found.");

			var backgroundTemplate = await backgroundRepository.FindAsync(character.Snapshot.Background.Id, cancellationToken: cancellationToken)
				?? throw new BusinessLogicException("No background with provided ID found.");

			var classTemplate = await classRepository.FindAsync(character.Snapshot.Class.Id, cancellationToken: cancellationToken)
				?? throw new BusinessLogicException("No class with provided ID found.");

			var generalConverter = new GeneralConverter(character, generalTemplate);
			var ancestryConverter = new AncestryConverter(character, ancestryTemplate);
			var backgroundConverter = new BackgroundConverter(character, backgroundTemplate);
			var classConverter = new ClassConverter(character, ancestryTemplate, classTemplate);

			return new()
			{
				Level = character.Snapshot.Level,
				General = generalConverter.Restore(),
				Ancestry = ancestryConverter.Restore(),
				Background = backgroundConverter.Restore(),
				Class = classConverter.Restore(),
			};
		}

		public async Task<Character> ApplyBuild(Guid characterId, BuildDetails details, CancellationToken cancellationToken = default)
		{
			var character = await characterRepository.FindAsync(characterId, cancellationToken: cancellationToken)
				?? throw new BusinessLogicException("No character with provided ID found.");

			var generalConverter = new GeneralConverter(character, details.General);
			var ancestryConverter = new AncestryConverter(character, details.Ancestry);
			var backgroundConverter = new BackgroundConverter(character, details.Background);
			var classConverter = new ClassConverter(character, details.Ancestry, details.Class);

			character.Snapshot.Level = details.Level;
			character.Snapshot.General = generalConverter.Snapshot();
			character.Snapshot.Ancestry = ancestryConverter.Snapshot();
			character.Snapshot.Background = backgroundConverter.Snapshot();
			character.Snapshot.Class = classConverter.Snapshot();

			character.Build.Level = character.Snapshot.Level;
			character.Build.General = generalConverter.Convert();
			character.Build.Ancestry = ancestryConverter.Convert();
			character.Build.Background = backgroundConverter.Convert();
			character.Build.Class = classConverter.Convert();

			characterRepository.Save(character);
			await characterRepository.CommitAsync(cancellationToken);

			return character;
		}

		public async Task<List<Character>> GetAllCharactersAsync(CancellationToken cancellationToken)
		{
			var characters = await characterRepository.GetAllCharactersAsync(cancellationToken);
			var ancestryTemplates = await ancestryRepository.GetAllAncestriesAsync(cancellationToken);
			var backgroundTemplates = await backgroundRepository.GetAllBackgroundsAsync(cancellationToken);
			var classTemplates = await classRepository.GetAllClassesAsync(cancellationToken);

			foreach (var character in characters)
			{
				RestoreCharacter(character, ancestryTemplates, backgroundTemplates, classTemplates);
			}

			return characters;
		}

		public async Task<Character> GetCharacterAsync(Guid id, CancellationToken cancellationToken)
		{
			var character = await characterRepository.FindAsync(id, cancellationToken: cancellationToken)
				?? throw new BusinessLogicException("No character with provided ID found.");

			var generalTemplate = new General();

			var ancestryTemplate = await ancestryRepository.FindAsync(character.Snapshot.Ancestry.Id, cancellationToken: cancellationToken)
				?? throw new BusinessLogicException("No ancestry with provided ID found.");

			var backgroundTemplate = await backgroundRepository.FindAsync(character.Snapshot.Background.Id, cancellationToken: cancellationToken)
				?? throw new BusinessLogicException("No background with provided ID found.");

			var classTemplate = await classRepository.FindAsync(character.Snapshot.Class.Id, cancellationToken: cancellationToken)
				?? throw new BusinessLogicException("No class with provided ID found.");

			var generalConverter = new GeneralConverter(character, generalTemplate);
			var ancestryConverter = new AncestryConverter(character, ancestryTemplate);
			var backgroundConverter = new BackgroundConverter(character, backgroundTemplate);
			var classConverter = new ClassConverter(character, ancestryTemplate, classTemplate);

			character.Build.Level = character.Snapshot.Level;
			character.Build.General = generalConverter.Convert();
			character.Build.Ancestry = ancestryConverter.Convert();
			character.Build.Background = backgroundConverter.Convert();
			character.Build.Class = classConverter.Convert();

			return character;
		}

		private Character RestoreCharacter(
			Character character, 
			List<AncestryTemplate> ancestryTemplates, 
			List<BackgroundTemplate> backgroundTemplates, 
			List<ClassTemplate> classTemplates)
		{
			var generalTemplate = new General();

			var ancestryTemplate = ancestryTemplates.FirstOrDefault(x => x.Id == character.Snapshot.Ancestry.Id)
				?? throw new BusinessLogicException("No ancestry with provided ID found.");

			var backgroundTemplate = backgroundTemplates.FirstOrDefault(x => x.Id == character.Snapshot.Background.Id)
				?? throw new BusinessLogicException("No background with provided ID found.");

			var classTemplate = classTemplates.FirstOrDefault(x => x.Id == character.Snapshot.Class.Id)
				?? throw new BusinessLogicException("No class with provided ID found.");

			var generalConverter = new GeneralConverter(character, generalTemplate);
			var ancestryConverter = new AncestryConverter(character, ancestryTemplate);
			var backgroundConverter = new BackgroundConverter(character, backgroundTemplate);
			var classConverter = new ClassConverter(character, ancestryTemplate, classTemplate);

			character.Build.General = generalConverter.Convert();
			character.Build.Ancestry = ancestryConverter.Convert();
			character.Build.Background = backgroundConverter.Convert();
			character.Build.Class = classConverter.Convert();

			return character;
		}
	}
}
