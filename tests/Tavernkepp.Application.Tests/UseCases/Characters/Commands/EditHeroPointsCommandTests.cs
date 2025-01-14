using Moq;
using Tavernkeep.Application.Interfaces;
using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Entities.Pathfinder;
using Tavernkeep.Core.Entities;
using Tavernkeep.Core.Exceptions;
using Tavernkeep.Core.Repositories;
using Tavernkeep.Core.Specifications;
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
			var mockUserRepository = new Mock<IUserRepository>();
			var mockCharacterRepository = new Mock<ICharacterRepository>();
			var mockNotificationService = new Mock<INotificationService>();

			mockUserRepository
				.Setup(repo => repo.FindAsync(owner.Id, It.IsAny<ISpecification<User>>(), It.IsAny<CancellationToken>()!))
				.ReturnsAsync(owner);
			mockCharacterRepository
				.Setup(repo => repo.GetFullCharacterAsync(characterId, It.IsAny<CancellationToken>()))
				.ReturnsAsync(character);

			var request = new EditHeroPointsCommand(owner.Id, characterId, value);
			var handler = new EditHeroPointsCommandHandler(mockUserRepository.Object, mockCharacterRepository.Object, mockNotificationService.Object);

			await handler.Handle(request, CancellationToken.None);

			Assert.That(character.HeroPoints, Is.EqualTo(value));
		}

		[Test]
		public async Task EditHeroPointsCommand_Success_Master()
		{
			var mockUserRepository = new Mock<IUserRepository>();
			var mockCharacterRepository = new Mock<ICharacterRepository>();
			var mockNotificationService = new Mock<INotificationService>();

			mockUserRepository
				.Setup(repo => repo.FindAsync(master.Id, It.IsAny<ISpecification<User>>(), It.IsAny<CancellationToken>()))
				.ReturnsAsync(master);
			mockCharacterRepository
				.Setup(repo => repo.GetFullCharacterAsync(characterId, It.IsAny<CancellationToken>()))
				.ReturnsAsync(character);

			var request = new EditHeroPointsCommand(master.Id, characterId, 2);
			var handler = new EditHeroPointsCommandHandler(mockUserRepository.Object, mockCharacterRepository.Object, mockNotificationService.Object);

			await handler.Handle(request, CancellationToken.None);

			Assert.That(character.HeroPoints, Is.EqualTo(2));
		}

		[Test]
		public void EditHeroPointsCommand_BelowZero()
		{
			var mockUserRepository = new Mock<IUserRepository>();
			var mockCharacterRepository = new Mock<ICharacterRepository>();
			var mockNotificationService = new Mock<INotificationService>();

			mockUserRepository
				.Setup(repo => repo.FindAsync(master.Id, It.IsAny<ISpecification<User>>(), It.IsAny<CancellationToken>()))
				.ReturnsAsync(master);
			mockCharacterRepository
				.Setup(repo => repo.GetFullCharacterAsync(characterId, It.IsAny<CancellationToken>()))
				.ReturnsAsync(character);

			var request = new EditHeroPointsCommand(master.Id, characterId, -1);
			var handler = new EditHeroPointsCommandHandler(mockUserRepository.Object, mockCharacterRepository.Object, mockNotificationService.Object);

			Assert.ThatAsync(async () => await handler.Handle(request, CancellationToken.None),
				Throws.TypeOf<BusinessLogicException>()
				.With.Message.EqualTo("Hero points amount cannot be less than zero."));
		}

		[Test]
		public void EditHeroPointsCommand_AboveThree()
		{
			var mockUserRepository = new Mock<IUserRepository>();
			var mockCharacterRepository = new Mock<ICharacterRepository>();
			var mockNotificationService = new Mock<INotificationService>();

			mockUserRepository
				.Setup(repo => repo.FindAsync(master.Id, It.IsAny<ISpecification<User>>(), It.IsAny<CancellationToken>()))
				.ReturnsAsync(master);
			mockCharacterRepository
				.Setup(repo => repo.GetFullCharacterAsync(characterId, It.IsAny<CancellationToken>()))
				.ReturnsAsync(character);

			var request = new EditHeroPointsCommand(master.Id, characterId, 4);
			var handler = new EditHeroPointsCommandHandler(mockUserRepository.Object, mockCharacterRepository.Object, mockNotificationService.Object);

			Assert.ThatAsync(async () => await handler.Handle(request, CancellationToken.None),
				Throws.TypeOf<BusinessLogicException>()
				.With.Message.EqualTo("Hero points amoint cannot be more than three."));
		}

		[Test]
		public void EditHeroPointsCommand_InitiatorNotFound()
		{
			var mockUserRepository = new Mock<IUserRepository>();
			var mockCharacterRepository = new Mock<ICharacterRepository>();
			var mockNotificationService = new Mock<INotificationService>();

			mockCharacterRepository
				.Setup(repo => repo.GetFullCharacterAsync(characterId, It.IsAny<CancellationToken>()))
				.ReturnsAsync(character);

			var request = new EditHeroPointsCommand(owner.Id, characterId, 2);
			var handler = new EditHeroPointsCommandHandler(mockUserRepository.Object, mockCharacterRepository.Object, mockNotificationService.Object);

			Assert.ThatAsync(async () => await handler.Handle(request, CancellationToken.None),
				Throws.TypeOf<BusinessLogicException>()
				.With.Message.EqualTo("User with specified ID doesn't exist."));
		}

		[Test]
		public void EditHeroPointsCommand_CharacterNotFound()
		{
			var mockUserRepository = new Mock<IUserRepository>();
			var mockCharacterRepository = new Mock<ICharacterRepository>();
			var mockNotificationService = new Mock<INotificationService>();

			mockUserRepository
				.Setup(repo => repo.FindAsync(owner.Id, It.IsAny<ISpecification<User>>(), It.IsAny<CancellationToken>()))
				.ReturnsAsync(owner);

			var request = new EditHeroPointsCommand(owner.Id, characterId, 2);
			var handler = new EditHeroPointsCommandHandler(mockUserRepository.Object, mockCharacterRepository.Object, mockNotificationService.Object);

			Assert.ThatAsync(async () => await handler.Handle(request, CancellationToken.None),
				Throws.TypeOf<BusinessLogicException>()
				.With.Message.EqualTo("Character with specified ID doesn't exist."));
		}

		[Test]
		public void EditHeroPointsCommand_NotEnoughPermissions()
		{
			var mockUserRepository = new Mock<IUserRepository>();
			var mockCharacterRepository = new Mock<ICharacterRepository>();
			var mockNotificationService = new Mock<INotificationService>();
			var initiatorId = Guid.NewGuid();

			mockUserRepository
				.Setup(repo => repo.FindAsync(initiatorId, It.IsAny<ISpecification<User>>(), It.IsAny<CancellationToken>()))
				.ReturnsAsync(new User(string.Empty, string.Empty, UserRole.Player));
			mockCharacterRepository
				.Setup(repo => repo.GetFullCharacterAsync(characterId, It.IsAny<CancellationToken>()))
				.ReturnsAsync(character);

			var request = new EditHeroPointsCommand(initiatorId, characterId, 2);
			var handler = new EditHeroPointsCommandHandler(mockUserRepository.Object, mockCharacterRepository.Object, mockNotificationService.Object);

			Assert.ThatAsync(async () => await handler.Handle(request, CancellationToken.None),
				Throws.TypeOf<InsufficientPermissionException>()
				.With.Message.EqualTo("You do not have the necessary permissions to perform this operation."));
		}
	}
}
