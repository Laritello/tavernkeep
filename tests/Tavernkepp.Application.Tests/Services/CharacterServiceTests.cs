using Moq;
using Tavernkeep.Application.Interfaces;
using Tavernkeep.Application.Services;
using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Entities;
using Tavernkeep.Core.Entities.Pathfinder;
using Tavernkeep.Core.Exceptions;
using Tavernkeep.Core.Repositories;
using Tavernkeep.Core.Specifications;

namespace Tavernkepp.Application.Tests.Services
{
	public class CharacterServiceTests
	{
		private readonly Guid characterId = Guid.NewGuid();

		private readonly User owner;

		private Character character = default!;

		public CharacterServiceTests()
		{
			owner = new User("owner", "owner", UserRole.Player) { Id = Guid.NewGuid() };
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
		public void RetrieveCharacterForEditing_InitiatorNotFound()
		{
			var mockUserRepository = new Mock<IUserRepository>();
			var mockCharacterRepository = new Mock<ICharacterRepository>();
			var mockNotificationService = new Mock<INotificationService>();

			mockCharacterRepository
				.Setup(repo => repo.FindAsync(characterId, It.IsAny<ISpecification<Character>>(), It.IsAny<CancellationToken>()))
				.ReturnsAsync(character);

			var service = new CharacterService(mockCharacterRepository.Object, mockUserRepository.Object, mockNotificationService.Object);

			Assert.ThatAsync(async () => await service.RetrieveCharacterForEdit(characterId, Guid.NewGuid(), CancellationToken.None),
				Throws.TypeOf<BusinessLogicException>()
				.With.Message.EqualTo("User with specified ID doesn't exist."));
		}

		[Test]
		public void RetrieveCharacterForEditing_CharacterNotFound()
		{
			var mockUserRepository = new Mock<IUserRepository>();
			var mockCharacterRepository = new Mock<ICharacterRepository>();
			var mockNotificationService = new Mock<INotificationService>();

			mockUserRepository
				.Setup(repo => repo.FindAsync(owner.Id, It.IsAny<ISpecification<User>>(), It.IsAny<CancellationToken>()))
				.ReturnsAsync(owner);

			var service = new CharacterService(mockCharacterRepository.Object, mockUserRepository.Object, mockNotificationService.Object);

			Assert.ThatAsync(async () => await service.RetrieveCharacterForEdit(characterId, owner.Id, CancellationToken.None),
				Throws.TypeOf<BusinessLogicException>()
				.With.Message.EqualTo("Character with specified ID doesn't exist."));
		}

		[Test]
		public void RetrieveCharacterForEditing_NotEnoughPermissions()
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

			var service = new CharacterService(mockCharacterRepository.Object, mockUserRepository.Object, mockNotificationService.Object);

			Assert.ThatAsync(async () => await service.RetrieveCharacterForEdit(characterId, initiatorId, CancellationToken.None),
				Throws.TypeOf<InsufficientPermissionException>()
				.With.Message.EqualTo("You do not have the necessary permissions to perform this operation."));
		}
	}
}
