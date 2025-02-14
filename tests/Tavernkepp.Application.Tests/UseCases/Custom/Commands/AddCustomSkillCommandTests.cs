using Moq;
using Tavernkeep.Application.Interfaces;
using Tavernkeep.Application.UseCases.Custom.Commands.AddCustomSkill;
using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Entities;
using Tavernkeep.Core.Entities.Pathfinder;
using Tavernkeep.Core.Exceptions;

namespace Tavernkepp.Application.Tests.UseCases.Custom.Commands
{
	internal class AddCustomSkillCommandTests
	{
		private readonly Guid characterId = Guid.NewGuid();

		private readonly User owner;
		private readonly User master;

		private Character character = default!;


		public AddCustomSkillCommandTests()
		{
			owner = new User("owner", "owner", UserRole.Player) { Id = Guid.NewGuid() };
			master = new User("master", "master", UserRole.Master) { Id = Guid.NewGuid() };
		}

		[SetUp]
		public void SetUp()
		{
			character = CharacterGenerator.Generate(characterId, owner);
		}

		[Test]
		[TestCase("Custom", "Strength", SkillType.Custom)]
		[TestCase("Custom", "Dexterity", SkillType.Custom)]
		[TestCase("Custom", "Intelligence", SkillType.Custom)]
		[TestCase("Custom", "Constitution", SkillType.Custom)]
		[TestCase("Custom", "Wisdom", SkillType.Custom)]
		[TestCase("Custom", "Charisma", SkillType.Custom)]
		[TestCase("Lore", null, SkillType.Lore)]
		public async Task AddCustomSkillCommand_Success(string name, string? baseAbility, SkillType type)
		{
			var mockCharacterService = new Mock<ICharacterService>();

			mockCharacterService
				.Setup(s => s.RetrieveCharacterForAction(characterId, owner.Id, It.IsAny<CancellationToken>()))
				.ReturnsAsync(character);

			int skillsAmount = character.Skills.Count;

			var request = new AddCustomSkillCommand(owner.Id, characterId, name, baseAbility, type);
			var handler = new AddCustomSkillCommandHandler(mockCharacterService.Object);

			await handler.Handle(request, CancellationToken.None);

			Assert.Multiple(() =>
			{
				Assert.That(character.Skills, Has.Count.EqualTo(skillsAmount + 1));
				Assert.That(character.Skills.Any(x => x.Name == name), Is.True);
			});
		}

		[Test]
		[TestCase("Custom", "Strength", SkillType.Custom)]
		[TestCase("Lore", null, SkillType.Lore)]
		public async Task AddCustomSkillCommand_Success_Master(string name, string? baseAbility, SkillType type)
		{
			var mockCharacterService = new Mock<ICharacterService>();

			mockCharacterService
				.Setup(s => s.RetrieveCharacterForAction(characterId, master.Id, It.IsAny<CancellationToken>()))
				.ReturnsAsync(character);

			int skillsAmount = character.Skills.Count;

			var request = new AddCustomSkillCommand(master.Id, characterId, name, baseAbility, type);
			var handler = new AddCustomSkillCommandHandler(mockCharacterService.Object);

			await handler.Handle(request, CancellationToken.None);

			Assert.Multiple(() =>
			{
				Assert.That(character.Skills, Has.Count.EqualTo(skillsAmount + 1));
				Assert.That(character.Skills.Any(x => x.Name == name), Is.True);
			});
		}

		[Test]
		public void AddCustomSkillCommand_InvalidType()
		{
			var mockCharacterService = new Mock<ICharacterService>();

			mockCharacterService
				.Setup(s => s.RetrieveCharacterForAction(characterId, owner.Id, It.IsAny<CancellationToken>()))
				.ReturnsAsync(character);

			var request = new AddCustomSkillCommand(owner.Id, characterId, "Custom", "Intelligence", SkillType.Basic);
			var handler = new AddCustomSkillCommandHandler(mockCharacterService.Object);

			Assert.ThatAsync(async () => await handler.Handle(request, CancellationToken.None),
				Throws.TypeOf<BusinessLogicException>()
				.With.Message.EqualTo($"Invalid skill type: {nameof(request.Type)}"));
		}

		[Test]
		public void AddCustomSkillCommand_HasSameName()
		{
			var mockCharacterService = new Mock<ICharacterService>();

			mockCharacterService
				.Setup(s => s.RetrieveCharacterForAction(characterId, owner.Id, It.IsAny<CancellationToken>()))
				.ReturnsAsync(character);

			var request = new AddCustomSkillCommand(owner.Id, characterId, "Arcana", "Intelligence", SkillType.Custom);
			var handler = new AddCustomSkillCommandHandler(mockCharacterService.Object);

			Assert.ThatAsync(async () => await handler.Handle(request, CancellationToken.None),
				Throws.TypeOf<BusinessLogicException>()
				.With.Message.EqualTo("Character already has a skill with this name."));
		}

		[Test]
		public void AddCustomSkillCommand_InvalidBaseAblity()
		{
			var mockCharacterService = new Mock<ICharacterService>();

			mockCharacterService
				.Setup(s => s.RetrieveCharacterForAction(characterId, owner.Id, It.IsAny<CancellationToken>()))
				.ReturnsAsync(character);

			var request = new AddCustomSkillCommand(owner.Id, characterId, "Custom", "Wrong", SkillType.Custom);
			var handler = new AddCustomSkillCommandHandler(mockCharacterService.Object);

			Assert.ThatAsync(async () => await handler.Handle(request, CancellationToken.None),
				Throws.TypeOf<BusinessLogicException>()
				.With.Message.EqualTo("Character does not have an ability with this name."));
		}
	}
}
