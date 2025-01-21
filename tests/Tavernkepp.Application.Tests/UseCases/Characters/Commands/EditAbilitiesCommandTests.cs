using Moq;
using Tavernkeep.Application.Interfaces;
using Tavernkeep.Application.UseCases.Characters.Commands.EditAbilities;
using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Entities;
using Tavernkeep.Core.Entities.Pathfinder;

namespace Tavernkepp.Application.Tests.UseCases.Characters.Commands
{
	public class EditAbilitiesCommandTests
	{
		private readonly Guid characterId = Guid.NewGuid();

		private readonly User owner;
		private readonly User master;

		private Character character = default!;

		public EditAbilitiesCommandTests()
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
		[TestCase("Strength")]
		[TestCase("Dexterity")]
		[TestCase("Constitution")]
		[TestCase("Intelligence")]
		[TestCase("Wisdom")]
		[TestCase("Charisma")]
		public async Task EditAbilitiesCommand_Success(string type)
		{
			var mockCharacterService = new Mock<ICharacterService>();

			mockCharacterService
				.Setup(s => s.RetrieveCharacterForEdit(characterId, owner.Id, It.IsAny<CancellationToken>()))
				.ReturnsAsync(character);

			var newScore = 12;

			var request = new EditAbilitiesCommand(owner.Id, characterId, new() { { type, newScore } });
			var handler = new EditAbilitiesCommandHandler(mockCharacterService.Object);

			await handler.Handle(request, CancellationToken.None);

			Assert.That(character.Abilities[type].Score, Is.EqualTo(newScore));
		}

		[Test]
		public async Task EditAbilitiesCommand_Success_Master()
		{
			var mockCharacterService = new Mock<ICharacterService>();

			mockCharacterService
				.Setup(s => s.RetrieveCharacterForEdit(characterId, master.Id, It.IsAny<CancellationToken>()))
				.ReturnsAsync(character);

			var newScore = 12;

			var request = new EditAbilitiesCommand(master.Id, characterId, new() { { "Strength", newScore } });
			var handler = new EditAbilitiesCommandHandler(mockCharacterService.Object);

			await handler.Handle(request, CancellationToken.None);

			Assert.That(character.Abilities["Strength"].Score, Is.EqualTo(newScore));
		}
	}
}
