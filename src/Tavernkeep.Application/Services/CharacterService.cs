using Tavernkeep.Application.Interfaces;
using Tavernkeep.Application.UseCases.Characters.Notifications.CharacterCreated;
using Tavernkeep.Application.UseCases.Characters.Notifications.CharacterDeleted;
using Tavernkeep.Application.UseCases.Characters.Notifications.CharacterEdited;
using Tavernkeep.Core.Contracts.Character.Dtos;
using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Entities;
using Tavernkeep.Core.Entities.Pathfinder;
using Tavernkeep.Core.Entities.Pathfinder.Properties;
using Tavernkeep.Core.Exceptions;
using Tavernkeep.Core.Repositories;
using Tavernkeep.Core.Services;

namespace Tavernkeep.Application.Services
{
	// TODO: Optimize and refactor!!!
	public class CharacterService(
		ICharacterRepository characterRepository,
		IUserRepository userRepository,
		INotificationService notificationService
		) : ICharacterService
	{
		public async Task<Character> CreateCharacterAsync(User owner, CharacterTemplateDto characterData, CancellationToken cancellationToken = default)
		{
			Character character = new()
			{
				Owner = owner,
				Name = characterData.Name,
				Level = characterData.Level,
			};

			character.Ancestry = new(characterData.Ancestry.Name, characterData.Ancestry.Health)
			{
				Owner = character,
			};

			character.Class = new(characterData.Class.Name, characterData.Class.HealthPerLevel)
			{
				Owner = character
			};

			character.Abilities = [.. characterData.Abilities.Select(x => new Ability(x.Name, x.Score) { Owner = character })];

			character.Skills =
			[
				new("Acrobatics", Proficiency.Untrained, SkillType.Basic)
				{
					Owner = character,
					Ability = character.Abilities["Dexterity"]
				},
				new("Arcana", Proficiency.Untrained, SkillType.Basic)
				{
					Owner = character,
					Ability = character.Abilities["Intelligence"]
				},
				new("Athletics", Proficiency.Untrained, SkillType.Basic)
				{
					Owner = character,
					Ability = character.Abilities["Strength"]
				},
				new("Crafting", Proficiency.Untrained, SkillType.Basic)
				{
					Owner = character,
					Ability = character.Abilities["Intelligence"]
				},
				new("Deception", Proficiency.Untrained, SkillType.Basic)
				{
					Owner = character,
					Ability = character.Abilities["Charisma"]
				},
				new("Diplomacy", Proficiency.Untrained, SkillType.Basic)
				{
					Owner = character,
					Ability = character.Abilities["Charisma"]
				},
				new("Intimidation", Proficiency.Untrained, SkillType.Basic)
				{
					Owner = character,
					Ability = character.Abilities["Charisma"]
				},
				new("Medicine", Proficiency.Untrained, SkillType.Basic)
				{
					Owner = character,
					Ability = character.Abilities["Wisdom"]
				},
				new("Nature", Proficiency.Untrained, SkillType.Basic)
				{
					Owner = character,
					Ability = character.Abilities["Wisdom"]
				},
				new("Occultism", Proficiency.Untrained, SkillType.Basic)
				{
					Owner = character,
					Ability = character.Abilities["Intelligence"]
				},
				new("Performance", Proficiency.Untrained, SkillType.Basic)
				{
					Owner = character,
					Ability = character.Abilities["Charisma"]
				},
				new("Religion", Proficiency.Untrained, SkillType.Basic)
				{
					Owner = character,
					Ability = character.Abilities["Wisdom"]
				},
				new("Society", Proficiency.Untrained, SkillType.Basic)
				{
					Owner = character,
					Ability = character.Abilities["Intelligence"]
				},
				new("Stealth", Proficiency.Untrained, SkillType.Basic)
				{
					Owner = character,
					Ability = character.Abilities["Dexterity"]
				},
				new("Survival", Proficiency.Untrained, SkillType.Basic)
				{
					Owner = character,
					Ability = character.Abilities["Wisdom"]
				},
				new("Thievery", Proficiency.Untrained, SkillType.Basic)
				{
					Owner = character,
					Ability = character.Abilities["Dexterity"]
				},

				new("Fortitude", Proficiency.Untrained, SkillType.SavingThrow)
				{
					Owner = character,
					Ability = character.Abilities["Constitution"]
				},
				new("Reflex", Proficiency.Untrained, SkillType.SavingThrow)
				{
					Owner = character,
					Ability = character.Abilities["Dexterity"]
				},
				new("Will", Proficiency.Untrained, SkillType.SavingThrow)
				{
					Owner = character,
					Ability = character.Abilities["Wisdom"]
				},

				new("Perception", Proficiency.Untrained, SkillType.Perception)
				{
					Owner = character,
					Ability = character.Abilities["Wisdom"]
				},
			];

			foreach (var skill in characterData.Skills)
			{
				character.Skills[skill.Name].Proficiency = skill.Proficiency;
			}

			foreach (var savingThrow in characterData.SavingThrows)
			{
				character.Skills[savingThrow.Name].Proficiency = savingThrow.Proficiency;
			}

			character.Health = new()
			{
				Owner = character,
			};

			character.Health.Current = character.Health.Max;

			characterRepository.Save(character);
			await characterRepository.CommitAsync(cancellationToken);
			await notificationService.Publish(new CharacterCreatedNotification(character), cancellationToken);

			return character;
		}

		public Task<Character> CreateCharacterTemplateAsync(CancellationToken cancellationToken)
		{
			var character = new Character()
			{
				Name = "Unknown hero",
				Level = 1,
			};

			character.Ancestry = new("Human", 8)
			{
				Owner = character,
			};

			character.Class = new("Fighter", 10)
			{
				Owner = character
			};

			character.Abilities =
			[
				new("Strength", 10) { Owner = character },
				new("Dexterity", 10) { Owner = character },
				new("Constitution", 10) { Owner = character },
				new("Intelligence", 10) { Owner = character },
				new("Wisdom", 10) { Owner = character },
				new("Charisma", 10) { Owner = character }
			];

			character.Skills =
			[
				new("Acrobatics", Proficiency.Untrained, SkillType.Basic)
				{
					Owner = character,
					Ability = character.Abilities["Dexterity"]
				},
				new("Arcana", Proficiency.Untrained, SkillType.Basic)
				{
					Owner = character,
					Ability = character.Abilities["Intelligence"]
				},
				new("Athletics", Proficiency.Untrained, SkillType.Basic)
				{
					Owner = character,
					Ability = character.Abilities["Strength"]
				},
				new("Crafting", Proficiency.Untrained, SkillType.Basic)
				{
					Owner = character,
					Ability = character.Abilities["Intelligence"]
				},
				new("Deception", Proficiency.Untrained, SkillType.Basic)
				{
					Owner = character,
					Ability = character.Abilities["Charisma"]
				},
				new("Diplomacy", Proficiency.Untrained, SkillType.Basic)
				{
					Owner = character,
					Ability = character.Abilities["Charisma"]
				},
				new("Intimidation", Proficiency.Untrained, SkillType.Basic)
				{
					Owner = character,
					Ability = character.Abilities["Charisma"]
				},
				new("Medicine", Proficiency.Untrained, SkillType.Basic)
				{
					Owner = character,
					Ability = character.Abilities["Wisdom"]
				},
				new("Nature", Proficiency.Untrained, SkillType.Basic)
				{
					Owner = character,
					Ability = character.Abilities["Wisdom"]
				},
				new("Occultism", Proficiency.Untrained, SkillType.Basic)
				{
					Owner = character,
					Ability = character.Abilities["Intelligence"]
				},
				new("Performance", Proficiency.Untrained, SkillType.Basic)
				{
					Owner = character,
					Ability = character.Abilities["Charisma"]
				},
				new("Religion", Proficiency.Untrained, SkillType.Basic)
				{
					Owner = character,
					Ability = character.Abilities["Wisdom"]
				},
				new("Society", Proficiency.Untrained, SkillType.Basic)
				{
					Owner = character,
					Ability = character.Abilities["Intelligence"]
				},
				new("Stealth", Proficiency.Untrained, SkillType.Basic)
				{
					Owner = character,
					Ability = character.Abilities["Dexterity"]
				},
				new("Survival", Proficiency.Untrained, SkillType.Basic)
				{
					Owner = character,
					Ability = character.Abilities["Wisdom"]
				},
				new("Thievery", Proficiency.Untrained, SkillType.Basic)
				{
					Owner = character,
					Ability = character.Abilities["Dexterity"]
				},

				new("Fortitude", Proficiency.Untrained, SkillType.SavingThrow)
				{
					Owner = character,
					Ability = character.Abilities["Constitution"]
				},
				new("Reflex", Proficiency.Untrained, SkillType.SavingThrow)
				{
					Owner = character,
					Ability = character.Abilities["Dexterity"]
				},
				new("Will", Proficiency.Untrained, SkillType.SavingThrow)
				{
					Owner = character,
					Ability = character.Abilities["Wisdom"]
				},

				new("Perception", Proficiency.Untrained, SkillType.Perception)
				{
					Owner = character,
					Ability = character.Abilities["Wisdom"]
				},
			];

			character.Health = new()
			{
				Owner = character,
			};

			character.Walk.Base = 25;

			return Task.FromResult(character);
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

		public async Task<Character> RetrieveCharacterForEdit(Guid characterId, Guid userId, CancellationToken cancellationToken)
		{
			var initiator = await userRepository.FindAsync(userId, cancellationToken: cancellationToken)
				?? throw new BusinessLogicException("User with specified ID doesn't exist.");

			var character = await characterRepository.GetFullCharacterAsync(characterId, cancellationToken)
				?? throw new BusinessLogicException("Character with specified ID doesn't exist.");

			if (character.Owner.Id != userId && initiator.Role != UserRole.Master)
				throw new InsufficientPermissionException("You do not have the necessary permissions to perform this operation.");

			return character;
		}

		public async Task SaveCharacter(Character character, CancellationToken cancellationToken)
		{
			characterRepository.Save(character);
			await characterRepository.CommitAsync(cancellationToken);
			await notificationService.Publish(new CharacterEditedNotification(character), cancellationToken);
		}

		public async Task DeleteCharacter(Character character, CancellationToken cancellationToken)
		{
			characterRepository.Remove(character);
			await characterRepository.CommitAsync(cancellationToken);
			await notificationService.Publish(new CharacterDeletedNotification(character), cancellationToken);
		}
	}
}
