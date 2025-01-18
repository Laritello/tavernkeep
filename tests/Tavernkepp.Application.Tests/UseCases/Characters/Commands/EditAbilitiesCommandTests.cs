using Moq;
using Tavernkeep.Application.Interfaces;
using Tavernkeep.Application.UseCases.Characters.Commands.EditAbilities;
using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Entities;
using Tavernkeep.Core.Entities.Pathfinder;
using Tavernkeep.Core.Exceptions;
using Tavernkeep.Core.Repositories;
using Tavernkeep.Core.Specifications;

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
			var mockUserRepository = new Mock<IUserRepository>();
			var mockCharacterRepository = new Mock<ICharacterRepository>();
			var mockNotificationService = new Mock<INotificationService>();
			var newScore = 12;

			mockUserRepository
				.Setup(repo => repo.FindAsync(owner.Id, It.IsAny<ISpecification<User>>(), It.IsAny<CancellationToken>()!))
				.ReturnsAsync(owner);
			mockCharacterRepository
				.Setup(repo => repo.GetFullCharacterAsync(characterId, It.IsAny<CancellationToken>()))
				.ReturnsAsync(character);

			var request = new EditAbilitiesCommand(owner.Id, characterId, new() { { type, newScore } });
			var handler = new EditAbilitiesCommandHandler(mockUserRepository.Object, mockCharacterRepository.Object, mockNotificationService.Object);

			await handler.Handle(request, CancellationToken.None);

			Assert.That(character.Abilities[type].Score, Is.EqualTo(newScore));
		}

		[Test]
		public async Task EditAbilitiesCommand_Success_Master()
		{
			var mockUserRepository = new Mock<IUserRepository>();
			var mockCharacterRepository = new Mock<ICharacterRepository>();
			var mockNotificationService = new Mock<INotificationService>();
			var newScore = 12;

			mockUserRepository
				.Setup(repo => repo.FindAsync(master.Id, It.IsAny<ISpecification<User>>(), It.IsAny<CancellationToken>()))
				.ReturnsAsync(master);
			mockCharacterRepository
				.Setup(repo => repo.GetFullCharacterAsync(characterId, It.IsAny<CancellationToken>()))
				.ReturnsAsync(character);

			var request = new EditAbilitiesCommand(master.Id, characterId, new() { { "Strength", newScore } });
			var handler = new EditAbilitiesCommandHandler(mockUserRepository.Object, mockCharacterRepository.Object, mockNotificationService.Object);

			await handler.Handle(request, CancellationToken.None);

			Assert.That(character.Abilities["Strength"].Score, Is.EqualTo(newScore));
		}

		[Test]
		public void EditAbilitiesCommand_InitiatorNotFound()
		{
			var mockUserRepository = new Mock<IUserRepository>();
			var mockCharacterRepository = new Mock<ICharacterRepository>();
			var mockNotificationService = new Mock<INotificationService>();
			var newScore = 12;

			mockCharacterRepository
				.Setup(repo => repo.GetFullCharacterAsync(characterId, It.IsAny<CancellationToken>()))
				.ReturnsAsync(character);

			var request = new EditAbilitiesCommand(owner.Id, characterId, new() { { "Strength", newScore } });
			var handler = new EditAbilitiesCommandHandler(mockUserRepository.Object, mockCharacterRepository.Object, mockNotificationService.Object);

			Assert.ThatAsync(async () => await handler.Handle(request, CancellationToken.None),
				Throws.TypeOf<BusinessLogicException>()
				.With.Message.EqualTo("User with specified ID doesn't exist."));
		}

		[Test]
		public void EditAbilitiesCommand_CharacterNotFound()
		{
			var mockUserRepository = new Mock<IUserRepository>();
			var mockCharacterRepository = new Mock<ICharacterRepository>();
			var mockNotificationService = new Mock<INotificationService>();
			var newScore = 12;

			mockUserRepository
				.Setup(repo => repo.FindAsync(owner.Id, It.IsAny<ISpecification<User>>(), It.IsAny<CancellationToken>()))
				.ReturnsAsync(owner);

			var request = new EditAbilitiesCommand(owner.Id, characterId, new() { { "Strength", newScore } });
			var handler = new EditAbilitiesCommandHandler(mockUserRepository.Object, mockCharacterRepository.Object, mockNotificationService.Object);

			Assert.ThatAsync(async () => await handler.Handle(request, CancellationToken.None),
				Throws.TypeOf<BusinessLogicException>()
				.With.Message.EqualTo("Character with specified ID doesn't exist."));
		}

		[Test]
		public void EditAbilitiesCommand_NotEnoughPermissions()
		{
			var mockUserRepository = new Mock<IUserRepository>();
			var mockCharacterRepository = new Mock<ICharacterRepository>();
			var mockNotificationService = new Mock<INotificationService>();
			var newScore = 12;
			var initiatorId = Guid.NewGuid();

			mockUserRepository
				.Setup(repo => repo.FindAsync(initiatorId, It.IsAny<ISpecification<User>>(), It.IsAny<CancellationToken>()))
				.ReturnsAsync(new User(string.Empty, string.Empty, UserRole.Player));
			mockCharacterRepository
				.Setup(repo => repo.GetFullCharacterAsync(characterId, It.IsAny<CancellationToken>()))
				.ReturnsAsync(character);

			var request = new EditAbilitiesCommand(initiatorId, characterId, new() { { "Strength", newScore } });
			var handler = new EditAbilitiesCommandHandler(mockUserRepository.Object, mockCharacterRepository.Object, mockNotificationService.Object);

			Assert.ThatAsync(async () => await handler.Handle(request, CancellationToken.None),
				Throws.TypeOf<InsufficientPermissionException>()
				.With.Message.EqualTo("You do not have the necessary permissions to perform this operation."));
		}
	}
}
