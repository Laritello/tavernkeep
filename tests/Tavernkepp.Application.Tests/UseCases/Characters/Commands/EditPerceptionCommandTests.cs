using Moq;
using Tavernkeep.Application.Interfaces;
using Tavernkeep.Application.UseCases.Characters.Commands.EditPerception;
using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Entities;
using Tavernkeep.Core.Entities.Pathfinder;

namespace Tavernkepp.Application.Tests.UseCases.Characters.Commands
{
	public class EditPerceptionCommandTests
	{
		private readonly Guid characterId = Guid.NewGuid();
		private readonly Proficiency proficiency = Proficiency.Trained;

		private readonly User owner;
		private readonly User master;

		private Character character = default!;

		public EditPerceptionCommandTests()
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
		public async Task EditPerceptionCommand_Success()
		{
			var mockCharacterService = new Mock<ICharacterService>();

			mockCharacterService
				.Setup(s => s.RetrieveCharacterForAction(characterId, owner.Id, It.IsAny<CancellationToken>()))
				.ReturnsAsync(character);

			var request = new EditPerceptionCommand(owner.Id, characterId, proficiency);
			var handler = new EditPerceptionCommandHandler(mockCharacterService.Object);

			await handler.Handle(request, CancellationToken.None);

			Assert.That(character.Skills["Perception"].Proficiency, Is.EqualTo(proficiency));
		}

		[Test]
		public async Task EditPerceptionCommand_Success_Master()
		{
			var mockCharacterService = new Mock<ICharacterService>();

			mockCharacterService
				.Setup(s => s.RetrieveCharacterForAction(characterId, master.Id, It.IsAny<CancellationToken>()))
				.ReturnsAsync(character);

			var request = new EditPerceptionCommand(master.Id, characterId, proficiency);
			var handler = new EditPerceptionCommandHandler(mockCharacterService.Object);

			await handler.Handle(request, CancellationToken.None);

			Assert.That(character.Skills["Perception"].Proficiency, Is.EqualTo(proficiency));
		}
	}
}
