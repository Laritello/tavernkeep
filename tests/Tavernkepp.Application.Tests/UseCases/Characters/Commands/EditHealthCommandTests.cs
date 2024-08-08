using Moq;
using Tavernkeep.Application.Interfaces;
using Tavernkeep.Application.UseCases.Characters.Commands.EditHealth;
using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Entities;
using Tavernkeep.Core.Entities.Pathfinder;
using Tavernkeep.Core.Exceptions;
using Tavernkeep.Core.Repositories;
using Tavernkeep.Core.Specifications;

namespace Tavernkepp.Application.Tests.UseCases.Characters.Commands
{
	public class EditHealthCommandTests
	{
		private readonly Guid characterId = Guid.NewGuid();

		private readonly int currentHealth = 0;
		private readonly int maxHealth = 100;
		private readonly int tempHealth = 15;

		private readonly User owner;
		private readonly User master;

		private Character character;

		public EditHealthCommandTests()
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
		public async Task EditHealthCommand_Success()
		{
			var mockUserRepository = new Mock<IUserRepository>();
			var mockCharacterRepository = new Mock<ICharacterRepository>();
			var mockNotificationService = new Mock<INotificationService>();

			mockUserRepository
				.Setup(repo => repo.FindAsync(owner.Id, It.IsAny<ISpecification<User>>(), It.IsAny<CancellationToken>()))
				.ReturnsAsync(owner);
			mockCharacterRepository
				.Setup(repo => repo.GetFullCharacterAsync(characterId, It.IsAny<CancellationToken>()))
				.ReturnsAsync(character);

			var request = new EditHealthCommand(owner.Id, characterId, currentHealth, maxHealth, tempHealth);
			var handler = new EditHealthCommandHandler(mockUserRepository.Object, mockCharacterRepository.Object, mockNotificationService.Object);

			var response = await handler.Handle(request, CancellationToken.None);

			Assert.Multiple(() =>
			{
				Assert.That(response.Current, Is.EqualTo(currentHealth));
				Assert.That(response.Max, Is.EqualTo(maxHealth));
				Assert.That(response.Temporary, Is.EqualTo(tempHealth));
			});
		}

		[Test]
		public async Task EditHealthCommand_Success_Master()
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

			var request = new EditHealthCommand(master.Id, characterId, currentHealth, maxHealth, tempHealth);
			var handler = new EditHealthCommandHandler(mockUserRepository.Object, mockCharacterRepository.Object, mockNotificationService.Object);

			var response = await handler.Handle(request, CancellationToken.None);

			Assert.Multiple(() =>
			{
				Assert.That(response.Current, Is.EqualTo(currentHealth));
				Assert.That(response.Max, Is.EqualTo(maxHealth));
				Assert.That(response.Temporary, Is.EqualTo(tempHealth));
			});
		}

		[Test]
		public void EditHealthCommand_InitiatorNotFound()
		{
			var mockUserRepository = new Mock<IUserRepository>();
			var mockCharacterRepository = new Mock<ICharacterRepository>();
			var mockNotificationService = new Mock<INotificationService>();

			mockCharacterRepository
				.Setup(repo => repo.GetFullCharacterAsync(characterId, It.IsAny<CancellationToken>()))
				.ReturnsAsync(character);

			var request = new EditHealthCommand(owner.Id, characterId, currentHealth, maxHealth, tempHealth);
			var handler = new EditHealthCommandHandler(mockUserRepository.Object, mockCharacterRepository.Object, mockNotificationService.Object);

			var ex = Assert.ThrowsAsync<BusinessLogicException>(async () => await handler.Handle(request, CancellationToken.None));
			Assert.That(ex.Message, Is.EqualTo("User with specified ID doesn't exist."));
		}

		[Test]
		public void EditHealthCommand_NegativeMaxHealth()
		{
			var mockUserRepository = new Mock<IUserRepository>();
			var mockCharacterRepository = new Mock<ICharacterRepository>();
			var mockNotificationService = new Mock<INotificationService>();

			mockUserRepository
				.Setup(repo => repo.FindAsync(owner.Id, It.IsAny<ISpecification<User>>(), It.IsAny<CancellationToken>()))
				.ReturnsAsync(owner);
			mockCharacterRepository
				.Setup(repo => repo.GetFullCharacterAsync(characterId, It.IsAny<CancellationToken>()))
				.ReturnsAsync(character);

			var request = new EditHealthCommand(owner.Id, characterId, currentHealth, -1, tempHealth);
			var handler = new EditHealthCommandHandler(mockUserRepository.Object, mockCharacterRepository.Object, mockNotificationService.Object);

			var ex = Assert.ThrowsAsync<BusinessLogicException>(async () => await handler.Handle(request, CancellationToken.None));
			Assert.That(ex.Message, Is.EqualTo($"{nameof(request.Max)} can't be below zero."));
		}

		[Test]
		public void EditHealthCommand_NegativeCurrentHealth()
		{
			var mockUserRepository = new Mock<IUserRepository>();
			var mockCharacterRepository = new Mock<ICharacterRepository>();
			var mockNotificationService = new Mock<INotificationService>();

			mockUserRepository
				.Setup(repo => repo.FindAsync(owner.Id, It.IsAny<ISpecification<User>>(), It.IsAny<CancellationToken>()))
				.ReturnsAsync(owner);
			mockCharacterRepository
				.Setup(repo => repo.GetFullCharacterAsync(characterId, It.IsAny<CancellationToken>()))
				.ReturnsAsync(character);

			var request = new EditHealthCommand(owner.Id, characterId, -1, maxHealth, tempHealth);
			var handler = new EditHealthCommandHandler(mockUserRepository.Object, mockCharacterRepository.Object, mockNotificationService.Object);

			var ex = Assert.ThrowsAsync<BusinessLogicException>(async () => await handler.Handle(request, CancellationToken.None));
			Assert.That(ex.Message, Is.EqualTo($"{nameof(request.Current)} can't be below zero."));
		}

		[Test]
		public void EditHealthCommand_NegativeTemporaryHealth()
		{
			var mockUserRepository = new Mock<IUserRepository>();
			var mockCharacterRepository = new Mock<ICharacterRepository>();
			var mockNotificationService = new Mock<INotificationService>();

			mockUserRepository
				.Setup(repo => repo.FindAsync(owner.Id, It.IsAny<ISpecification<User>>(), It.IsAny<CancellationToken>()))
				.ReturnsAsync(owner);
			mockCharacterRepository
				.Setup(repo => repo.GetFullCharacterAsync(characterId, It.IsAny<CancellationToken>()))
				.ReturnsAsync(character);

			var request = new EditHealthCommand(owner.Id, characterId, currentHealth, maxHealth, -1);
			var handler = new EditHealthCommandHandler(mockUserRepository.Object, mockCharacterRepository.Object, mockNotificationService.Object);

			var ex = Assert.ThrowsAsync<BusinessLogicException>(async () => await handler.Handle(request, CancellationToken.None));
			Assert.That(ex.Message, Is.EqualTo($"{nameof(request.Temporary)} can't be below zero."));
		}

		[Test]
		public void EditHealthCommand_CharacterNotFound()
		{
			var mockUserRepository = new Mock<IUserRepository>();
			var mockCharacterRepository = new Mock<ICharacterRepository>();
			var mockNotificationService = new Mock<INotificationService>();

			mockUserRepository
				.Setup(repo => repo.FindAsync(owner.Id, It.IsAny<ISpecification<User>>(), It.IsAny<CancellationToken>()))
				.ReturnsAsync(owner);

			var request = new EditHealthCommand(owner.Id, characterId, currentHealth, maxHealth, tempHealth);
			var handler = new EditHealthCommandHandler(mockUserRepository.Object, mockCharacterRepository.Object, mockNotificationService.Object);

			var ex = Assert.ThrowsAsync<BusinessLogicException>(async () => await handler.Handle(request, CancellationToken.None));
			Assert.That(ex.Message, Is.EqualTo("Character with specified ID doesn't exist."));
		}

		[Test]
		public void EditHealthCommand_NotEnoughPermissions()
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

			var request = new EditHealthCommand(initiatorId, characterId, currentHealth, maxHealth, tempHealth);
			var handler = new EditHealthCommandHandler(mockUserRepository.Object, mockCharacterRepository.Object, mockNotificationService.Object);

			var ex = Assert.ThrowsAsync<InsufficientPermissionException>(async () => await handler.Handle(request, CancellationToken.None));
			Assert.That(ex.Message, Is.EqualTo("You do not have the necessary permissions to perform this operation."));
		}
	}
}
