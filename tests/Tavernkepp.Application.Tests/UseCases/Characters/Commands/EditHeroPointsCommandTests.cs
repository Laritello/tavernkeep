using Moq;
using Tavernkeep.Application.Interfaces;
using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Entities.Pathfinder;
using Tavernkeep.Core.Entities;
using Tavernkeep.Core.Exceptions;
using Tavernkeep.Application.UseCases.Characters.Commands.EditHeroPoints;

namespace Tavernkepp.Application.Tests.UseCases.Characters.Commands
{
	public class EditHeroPointsCommandTests
	{
		private readonly Guid characterId = Guid.NewGuid();

		private readonly User owner;
		private readonly User master;

		private Character character = default!;

		public EditHeroPointsCommandTests()
		{
			owner = new User("owner", "owner", UserRole.Player) { Id = Guid.NewGuid() };
			master = new User("master", "master", UserRole.Master) { Id = Guid.NewGuid() };
		}

		[SetUp]
		public void SetUp()
		{
			character = new Character()
			{
				Id = characterId,
				Name = "Demo",
				Owner = owner
			};
		}

		[Test]
		[TestCase(1)]
		[TestCase(2)]
		[TestCase(3)]
		public async Task EditHeroPointsCommand_Success(int value)
		{
			var mockCharacterService = new Mock<ICharacterService>();

			mockCharacterService
				.Setup(s => s.RetrieveCharacterForEdit(characterId, owner.Id, It.IsAny<CancellationToken>()))
				.ReturnsAsync(character);

			var request = new EditHeroPointsCommand(owner.Id, characterId, value);
			var handler = new EditHeroPointsCommandHandler(mockCharacterService.Object);

			await handler.Handle(request, CancellationToken.None);

			Assert.That(character.HeroPoints, Is.EqualTo(value));
		}

		[Test]
		public async Task EditHeroPointsCommand_Success_Master()
		{
			var mockCharacterService = new Mock<ICharacterService>();

			mockCharacterService
				.Setup(s => s.RetrieveCharacterForEdit(characterId, master.Id, It.IsAny<CancellationToken>()))
				.ReturnsAsync(character);

			var request = new EditHeroPointsCommand(master.Id, characterId, 2);
			var handler = new EditHeroPointsCommandHandler(mockCharacterService.Object);

			await handler.Handle(request, CancellationToken.None);

			Assert.That(character.HeroPoints, Is.EqualTo(2));
		}

		[Test]
		public void EditHeroPointsCommand_BelowZero()
		{
			var mockCharacterService = new Mock<ICharacterService>();

			mockCharacterService
				.Setup(s => s.RetrieveCharacterForEdit(characterId, owner.Id, It.IsAny<CancellationToken>()))
				.ReturnsAsync(character);

			var request = new EditHeroPointsCommand(owner.Id, characterId, -1);
			var handler = new EditHeroPointsCommandHandler(mockCharacterService.Object);

			Assert.ThatAsync(async () => await handler.Handle(request, CancellationToken.None),
				Throws.TypeOf<BusinessLogicException>()
				.With.Message.EqualTo("Hero points amount cannot be less than zero."));
		}

		[Test]
		public void EditHeroPointsCommand_AboveThree()
		{
			var mockCharacterService = new Mock<ICharacterService>();

			mockCharacterService
				.Setup(s => s.RetrieveCharacterForEdit(characterId, owner.Id, It.IsAny<CancellationToken>()))
				.ReturnsAsync(character);

			var request = new EditHeroPointsCommand(owner.Id, characterId, 4);
			var handler = new EditHeroPointsCommandHandler(mockCharacterService.Object);

			Assert.ThatAsync(async () => await handler.Handle(request, CancellationToken.None),
				Throws.TypeOf<BusinessLogicException>()
				.With.Message.EqualTo("Hero points amoint cannot be more than three."));
		}
	}
}
