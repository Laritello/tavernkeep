using Moq;
using Tavernkeep.Application.Interfaces;
using Tavernkeep.Application.UseCases.Characters.Commands.EditSkills;
using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Entities;
using Tavernkeep.Core.Entities.Pathfinder;

namespace Tavernkepp.Application.Tests.UseCases.Characters.Commands
{
	public class EditSkillsCommandTests
	{
		private readonly Guid characterId = Guid.NewGuid();
		private readonly Proficiency proficiency = Proficiency.Trained;

		private readonly User owner;
		private readonly User master;

		private Character character = default!;

		public EditSkillsCommandTests()
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
		public async Task EditSkillsCommand_Success()
		{
			var mockCharacterService = new Mock<ICharacterService>();

			mockCharacterService
				.Setup(s => s.RetrieveCharacterForEdit(characterId, owner.Id, It.IsAny<CancellationToken>()))
				.ReturnsAsync(character);

			var request = new EditSkillsCommand(owner.Id, characterId, new() { { "Acrobatics", proficiency } });
			var handler = new EditSkillsCommandHandler(mockCharacterService.Object);

			await handler.Handle(request, CancellationToken.None);

			Assert.That(character.Skills["Acrobatics"].Proficiency, Is.EqualTo(proficiency));
		}

		[Test]
		[TestCase("Acrobatics")]
		[TestCase("Arcana")]
		[TestCase("Athletics")]
		[TestCase("Crafting")]
		[TestCase("Deception")]
		[TestCase("Diplomacy")]
		[TestCase("Intimidation")]
		[TestCase("Medicine")]
		[TestCase("Nature")]
		[TestCase("Occultism")]
		[TestCase("Performance")]
		[TestCase("Religion")]
		[TestCase("Society")]
		[TestCase("Stealth")]
		[TestCase("Survival")]
		[TestCase("Thievery")]
		public async Task EditSkillsCommand_Success_Master(string type)
		{
			var mockCharacterService = new Mock<ICharacterService>();

			mockCharacterService
				.Setup(s => s.RetrieveCharacterForEdit(characterId, master.Id, It.IsAny<CancellationToken>()))
				.ReturnsAsync(character);

			var request = new EditSkillsCommand(master.Id, characterId, new() { { type, proficiency } });
			var handler = new EditSkillsCommandHandler(mockCharacterService.Object);

			await handler.Handle(request, CancellationToken.None);

			Assert.That(character.Skills[type].Proficiency, Is.EqualTo(proficiency));
		}
	}
}
