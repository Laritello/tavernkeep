using Moq;
using Tavernkeep.Application.Interfaces;
using Tavernkeep.Application.UseCases.Characters.Commands.EditSavingThrows;
using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Entities;
using Tavernkeep.Core.Entities.Pathfinder;

namespace Tavernkepp.Application.Tests.UseCases.Characters.Commands
{
	public class EditSavingThrowsCommandTests
	{
		private readonly Guid characterId = Guid.NewGuid();
		private readonly Proficiency proficiency = Proficiency.Trained;

		private readonly User owner;
		private readonly User master;

		private Character character = default!;

		public EditSavingThrowsCommandTests()
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
		[TestCase("Fortitude")]
		[TestCase("Reflex")]
		[TestCase("Will")]
		public async Task EditSavingThrowsCommand_Success(string type)
		{
			var mockCharacterService = new Mock<ICharacterService>();

			mockCharacterService
				.Setup(s => s.RetrieveCharacterForAction(characterId, owner.Id, It.IsAny<CancellationToken>()))
				.ReturnsAsync(character);

			var request = new EditSavingThrowsCommand(owner.Id, characterId, new() { { type, proficiency } });
			var handler = new EditSavingThrowsCommandHandler(mockCharacterService.Object);

			await handler.Handle(request, CancellationToken.None);

			Assert.That(character.Skills[type].Proficiency, Is.EqualTo(proficiency));
		}

		[Test]
		public async Task EditSavingThrowsCommand_Success_Master()
		{
			var mockCharacterService = new Mock<ICharacterService>();

			mockCharacterService
				.Setup(s => s.RetrieveCharacterForAction(characterId, master.Id, It.IsAny<CancellationToken>()))
				.ReturnsAsync(character);

			var request = new EditSavingThrowsCommand(master.Id, characterId, new() { { "Fortitude", proficiency } });
			var handler = new EditSavingThrowsCommandHandler(mockCharacterService.Object);

			await handler.Handle(request, CancellationToken.None);

			Assert.That(character.Skills["Fortitude"].Proficiency, Is.EqualTo(proficiency));
		}
	}
}
