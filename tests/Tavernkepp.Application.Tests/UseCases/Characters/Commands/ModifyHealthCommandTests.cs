using Moq;
using Tavernkeep.Application.Interfaces;
using Tavernkeep.Application.UseCases.Characters.Commands.ModifyHealth;
using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Entities;
using Tavernkeep.Core.Entities.Pathfinder;

namespace Tavernkepp.Application.Tests.UseCases.Characters.Commands
{
	public class ModifyHealthCommandTests
	{
		private readonly Guid characterId = Guid.NewGuid();

		private readonly User owner;
		private readonly User master;

		private Character character = default!;

		public ModifyHealthCommandTests()
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
		[TestCase(-3, 8, 7)]
		[TestCase(-10, 8, 0)]
		[TestCase(-12, 6, 0)]
		[TestCase(0, 8, 10)]
		[TestCase(2, 10, 10)]
		[TestCase(10, 18, 10)]
		public async Task ModifyHealthCommand_Success(int change, int newCurrent, int newTemporary)
		{
			var mockCharacterService = new Mock<ICharacterService>();

			mockCharacterService
				.Setup(s => s.RetrieveCharacterForEdit(characterId, owner.Id, It.IsAny<CancellationToken>()))
				.ReturnsAsync(character);

			character.Health.Temporary = 10;
			character.Health.Current = 8;

			var request = new ModifyHealthCommand(owner.Id, characterId, change);
			var handler = new ModifyHealthCommandHandler(mockCharacterService.Object);

			await handler.Handle(request, CancellationToken.None);

			Assert.Multiple(() =>
			{
				Assert.That(character.Health.Current, Is.EqualTo(newCurrent));
				Assert.That(character.Health.Temporary, Is.EqualTo(newTemporary));
			});
		}

		[Test]
		public async Task ModifyHealthCommand_Success_Master()
		{
			var mockCharacterService = new Mock<ICharacterService>();

			mockCharacterService
				.Setup(s => s.RetrieveCharacterForEdit(characterId, master.Id, It.IsAny<CancellationToken>()))
				.ReturnsAsync(character);

			character.Health.Temporary = 4;
			character.Health.Current = 10;

			var request = new ModifyHealthCommand(master.Id, characterId, -10);
			var handler = new ModifyHealthCommandHandler(mockCharacterService.Object);

			await handler.Handle(request, CancellationToken.None);

			Assert.Multiple(() =>
			{
				Assert.That(character.Health.Current, Is.EqualTo(4));
				Assert.That(character.Health.Temporary, Is.EqualTo(0));
			});
		}
	}
}
