using Moq;
using Tavernkeep.Application.Interfaces;
using Tavernkeep.Application.UseCases.Custom.Commands.DeleteCustomSkill;
using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Entities;
using Tavernkeep.Core.Entities.Pathfinder;
using Tavernkeep.Core.Exceptions;

namespace Tavernkepp.Application.Tests.UseCases.Custom.Commands
{
	internal class DeleteCustomSkillCommandTests
	{
		private readonly Guid characterId = Guid.NewGuid();

		private readonly User owner;
		private readonly User master;

		private Character character = default!;


		public DeleteCustomSkillCommandTests()
		{
			owner = new User("owner", "owner", UserRole.Player) { Id = Guid.NewGuid() };
			master = new User("master", "master", UserRole.Master) { Id = Guid.NewGuid() };
		}

		[SetUp]
		public void SetUp()
		{
			character = CharacterGenerator.Generate(characterId, owner);
			character.Skills.Add(new("Custom", Proficiency.Untrained, SkillType.Custom)
			{
				Owner = character,
				Ability = character.Abilities["Intelligence"]
			});
		}

		[Test]
		public async Task DeleteCustomSkillCommand_Success()
		{
			var mockCharacterService = new Mock<ICharacterService>();

			mockCharacterService
				.Setup(s => s.RetrieveCharacterForEdit(characterId, owner.Id, It.IsAny<CancellationToken>()))
				.ReturnsAsync(character);

			int skillsAmount = character.Skills.Count;

			var request = new DeleteCustomSkillCommand(owner.Id, characterId, "Custom");
			var handler = new DeleteCustomSkillCommandHandler(mockCharacterService.Object);

			await handler.Handle(request, CancellationToken.None);

			Assert.Multiple(() =>
			{
				Assert.That(character.Skills, Has.Count.EqualTo(skillsAmount - 1));
				Assert.That(character.Skills.Any(x => x.Name == "Custom"), Is.False);
			});
		}

		[Test]
		public async Task DeleteCustomSkillCommand_Success_Master()
		{
			var mockCharacterService = new Mock<ICharacterService>();

			mockCharacterService
				.Setup(s => s.RetrieveCharacterForEdit(characterId, master.Id, It.IsAny<CancellationToken>()))
				.ReturnsAsync(character);

			int skillsAmount = character.Skills.Count;

			var request = new DeleteCustomSkillCommand(master.Id, characterId, "Custom");
			var handler = new DeleteCustomSkillCommandHandler(mockCharacterService.Object);

			await handler.Handle(request, CancellationToken.None);

			Assert.Multiple(() =>
			{
				Assert.That(character.Skills, Has.Count.EqualTo(skillsAmount - 1));
				Assert.That(character.Skills.Any(x => x.Name == "Custom"), Is.False);
			});
		}

		[Test]
		public void DeleteCustomSkillCommand_NoSkill()
		{
			var mockCharacterService = new Mock<ICharacterService>();

			mockCharacterService
				.Setup(s => s.RetrieveCharacterForEdit(characterId, owner.Id, It.IsAny<CancellationToken>()))
				.ReturnsAsync(character);

			var request = new DeleteCustomSkillCommand(owner.Id, characterId, "Wrong");
			var handler = new DeleteCustomSkillCommandHandler(mockCharacterService.Object);

			Assert.ThatAsync(async () => await handler.Handle(request, CancellationToken.None),
				Throws.TypeOf<BusinessLogicException>()
				.With.Message.EqualTo("Character does not have a skill with this name."));
		}

		[Test]
		public void DeleteCustomSkillCommand_InvalidType()
		{
			var mockCharacterService = new Mock<ICharacterService>();

			mockCharacterService
				.Setup(s => s.RetrieveCharacterForEdit(characterId, owner.Id, It.IsAny<CancellationToken>()))
				.ReturnsAsync(character);

			var request = new DeleteCustomSkillCommand(owner.Id, characterId, "Arcana");
			var handler = new DeleteCustomSkillCommandHandler(mockCharacterService.Object);

			Assert.ThatAsync(async () => await handler.Handle(request, CancellationToken.None),
				Throws.TypeOf<BusinessLogicException>()
				.With.Message.EqualTo("Deteled skill must either have custom or lore type."));
		}
	}
}
