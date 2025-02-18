using Moq;
using Tavernkeep.Application.Interfaces;
using Tavernkeep.Application.UseCases.Characters.Commands.CreateCharacter;
using Tavernkeep.Core.Contracts.Character.Dtos;
using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Entities;
using Tavernkeep.Core.Entities.Pathfinder;
using Tavernkeep.Core.Exceptions;
using Tavernkeep.Core.Repositories;
using Tavernkeep.Core.Services;
using Tavernkeep.Core.Specifications;

namespace Tavernkepp.Application.Tests.UseCases.Characters.Commands
{
	public class CreateCharacterCommandTests
	{
		private readonly Guid userId = Guid.NewGuid();

		private readonly string name = "default_character";
		private readonly User owner;
		private readonly Character character;
		private readonly CharacterTemplateDto template;
		public CreateCharacterCommandTests()
		{
			owner = new(string.Empty, string.Empty, UserRole.Player) { Id = userId };
			character = new() { Id = Guid.NewGuid(), Name = name, Owner = owner };
			template = new()
			{
				Name = name,
				Level = 3,
				Ancestry = new AncestryDto
				{
					Name = "Human",
					Health = 8
				},
				Class = new ClassDto()
				{
					Name = "Wizzard",
					HealthPerLevel = 6,
				},
				Abilities =
				[
					new() { Name = "Strength", Score = 8},
					new() { Name = "Dexterity", Score = 10 },
					new() { Name = "Intelligence", Score = 18 },
					new() { Name = "Constitution", Score = 16 },
					new() { Name = "Wisdom", Score = 14 },
					new() { Name = "Charisma", Score = 10 },
				],
				Skills =
				[
					new()
					{
						Name = "Acrobatics",
						Proficiency = Proficiency.Untrained,
						Type = SkillType.Basic
					},
					new()
					{
						Name = "Arcana",
						Proficiency = Proficiency.Untrained,
						Type = SkillType.Basic
					},
					new()
					{
						Name = "Athletics",
						Proficiency = Proficiency.Untrained,
						Type = SkillType.Basic
					},
					new()
					{
						Name = "Crafting",
						Proficiency = Proficiency.Untrained,
						Type = SkillType.Basic
					},
					new()
					{
						Name = "Deception",
						Proficiency = Proficiency.Untrained,
						Type = SkillType.Basic
					},
					new()
					{
						Name = "Diplomacy",
						Proficiency = Proficiency.Untrained,
						Type = SkillType.Basic
					},
					new()
					{
						Name = "Intimidation",
						Proficiency = Proficiency.Untrained,
						Type = SkillType.Basic
					},
					new()
					{
						Name = "Medicine",
						Proficiency = Proficiency.Untrained,
						Type = SkillType.Basic
					},
					new()
					{
						Name = "Nature",
						Proficiency = Proficiency.Untrained,
						Type = SkillType.Basic
					},
					new()
					{
						Name = "Occultism",
						Proficiency = Proficiency.Untrained,
						Type = SkillType.Basic
					},
					new()
					{
						Name = "Performance",
						Proficiency = Proficiency.Untrained,
						Type = SkillType.Basic
					},
					new()
					{
						Name = "Religion",
						Proficiency = Proficiency.Untrained,
						Type = SkillType.Basic
					},
					new()
					{
						Name = "Society",
						Proficiency = Proficiency.Untrained,
						Type = SkillType.Basic
					},
					new()
					{
						Name = "Stealth",
						Proficiency = Proficiency.Untrained,
						Type = SkillType.Basic
					},
					new()
					{
						Name = "Survival",
						Proficiency = Proficiency.Untrained,
						Type = SkillType.Basic
					},
					new()
					{
						Name = "Thievery",
						Proficiency = Proficiency.Untrained,
						Type = SkillType.Basic
					},
				],
				SavingThrows =
				[
					new()
					{
						Name = "Fortitude",
						Proficiency = Proficiency.Untrained,
					},
					new()
					{
						Name = "Reflex",
						Proficiency = Proficiency.Untrained,
					},
					new()
					{
						Name = "Will",
						Proficiency = Proficiency.Untrained,
					},
				],
				Perception = new() { Name = "Perception", Proficiency = Proficiency.Trained }
			};
		}

		[Test]
		public async Task CreateCharacterCommand_Success()
		{
			var mockUserRepository = new Mock<IUserRepository>();
			var mockCharacterService = new Mock<ICharacterService>();

			mockUserRepository
				.Setup(repo => repo.FindAsync(userId, It.IsAny<ISpecification<User>>(), It.IsAny<CancellationToken>()))
				.ReturnsAsync(owner);

			mockCharacterService
				.Setup(service => service.CreateCharacterAsync(owner, template, It.IsAny<CancellationToken>()))
				.ReturnsAsync(character);

			var request = new CreateCharacterCommand(userId, template);
			var handler = new CreateCharacterCommandHandler(mockUserRepository.Object, mockCharacterService.Object);

			var response = await handler.Handle(request, CancellationToken.None);

			Assert.Multiple(() =>
			{
				Assert.That(response.Name, Is.EqualTo(name));
				Assert.That(response.Owner.Id, Is.EqualTo(userId));
			});
		}

		[Test]
		public void CreateCharacterCommand_OwnerNotFound()
		{
			var mockUserRepository = new Mock<IUserRepository>();
			var mockCharacterService = new Mock<ICharacterService>();

			var request = new CreateCharacterCommand(userId, template);
			var handler = new CreateCharacterCommandHandler(mockUserRepository.Object, mockCharacterService.Object);

			Assert.ThatAsync(async () => await handler.Handle(request, CancellationToken.None),
				Throws.TypeOf<BusinessLogicException>()
				.With.Message.EqualTo("Owner with specified ID doesn't exist."));
		}
	}
}
