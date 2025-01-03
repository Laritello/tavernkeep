using Moq;
using Tavernkeep.Application.Interfaces;
using Tavernkeep.Application.UseCases.Characters.Commands.EditSavingThrow;
using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Entities;
using Tavernkeep.Core.Entities.Pathfinder;
using Tavernkeep.Core.Exceptions;
using Tavernkeep.Core.Repositories;
using Tavernkeep.Core.Specifications;

namespace Tavernkepp.Application.Tests.UseCases.Characters.Commands
{
	public class EditSavingThrowCommandTests
	{
		private readonly Guid characterId = Guid.NewGuid();
		private readonly Proficiency proficiency = Proficiency.Trained;

		private readonly User owner;
		private readonly User master;

		private Character character;

		public EditSavingThrowCommandTests()
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
		[TestCase(SavingThrowType.Fortitude)]
		[TestCase(SavingThrowType.Reflex)]
		[TestCase(SavingThrowType.Will)]
		public async Task EditSavingThrowCommand_Success(SavingThrowType type)
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

			var request = new EditSavingThrowCommand(owner.Id, characterId, type, proficiency);
			var handler = new EditSavingThrowsCommandHandler(mockUserRepository.Object, mockCharacterRepository.Object, mockNotificationService.Object);

			var response = await handler.Handle(request, CancellationToken.None);

			Assert.Multiple(() =>
			{
				Assert.That(response.Type, Is.EqualTo(type));
				Assert.That(response.Proficiency, Is.EqualTo(proficiency));
			});
		}

		[Test]
		public async Task EditSavingThrowCommand_Success_Master()
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

			var request = new EditSavingThrowCommand(master.Id, characterId, SavingThrowType.Fortitude, proficiency);
			var handler = new EditSavingThrowsCommandHandler(mockUserRepository.Object, mockCharacterRepository.Object, mockNotificationService.Object);

			var response = await handler.Handle(request, CancellationToken.None);

			Assert.That(response.Proficiency, Is.EqualTo(proficiency));
		}

		[Test]
		public void EditSavingThrowCommand_InitiatorNotFound()
		{
			var mockUserRepository = new Mock<IUserRepository>();
			var mockCharacterRepository = new Mock<ICharacterRepository>();
			var mockNotificationService = new Mock<INotificationService>();

			mockCharacterRepository
				.Setup(repo => repo.GetFullCharacterAsync(characterId, It.IsAny<CancellationToken>()))
				.ReturnsAsync(character);

			var request = new EditSavingThrowCommand(owner.Id, characterId, SavingThrowType.Fortitude, proficiency);
			var handler = new EditSavingThrowsCommandHandler(mockUserRepository.Object, mockCharacterRepository.Object, mockNotificationService.Object);

			var ex = Assert.ThrowsAsync<BusinessLogicException>(async () => await handler.Handle(request, CancellationToken.None));
			Assert.That(ex.Message, Is.EqualTo("User with specified ID doesn't exist."));
		}

		[Test]
		public void EditSavingThrowCommand_CharacterNotFound()
		{
			var mockUserRepository = new Mock<IUserRepository>();
			var mockCharacterRepository = new Mock<ICharacterRepository>();
			var mockNotificationService = new Mock<INotificationService>();

			mockUserRepository
				.Setup(repo => repo.FindAsync(owner.Id, It.IsAny<ISpecification<User>>(), It.IsAny<CancellationToken>()))
				.ReturnsAsync(owner);

			var request = new EditSavingThrowCommand(owner.Id, characterId, SavingThrowType.Fortitude, proficiency);
			var handler = new EditSavingThrowsCommandHandler(mockUserRepository.Object, mockCharacterRepository.Object, mockNotificationService.Object);

			var ex = Assert.ThrowsAsync<BusinessLogicException>(async () => await handler.Handle(request, CancellationToken.None));
			Assert.That(ex.Message, Is.EqualTo("Character with specified ID doesn't exist."));
		}

		[Test]
		public void EditSavingThrowCommand_NotEnoughPermissions()
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

			var request = new EditSavingThrowCommand(initiatorId, characterId, SavingThrowType.Fortitude, proficiency);
			var handler = new EditSavingThrowsCommandHandler(mockUserRepository.Object, mockCharacterRepository.Object, mockNotificationService.Object);

			var ex = Assert.ThrowsAsync<InsufficientPermissionException>(async () => await handler.Handle(request, CancellationToken.None));
			Assert.That(ex.Message, Is.EqualTo("You do not have the necessary permissions to perform this operation."));
		}
	}
}
