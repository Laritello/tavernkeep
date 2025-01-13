using Moq;
using Tavernkeep.Application.UseCases.Users.Commands.AssignCharacterToUser;
using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Entities;
using Tavernkeep.Core.Entities.Pathfinder;
using Tavernkeep.Core.Exceptions;
using Tavernkeep.Core.Repositories;
using Tavernkeep.Core.Specifications;

namespace Tavernkepp.Application.Tests.UseCases.Users.Commands
{
	public class AssignCharacterToUserCommandTests
	{
		private readonly Guid userId = Guid.NewGuid();
		private readonly Guid characterId = Guid.NewGuid();

		private readonly User owner;
		private Character character = default!;

		public AssignCharacterToUserCommandTests()
		{
			owner = new(string.Empty, string.Empty, UserRole.Player)
			{
				Id = userId,
			};
		}

		[SetUp]
		public void SetUp()
		{
			character = new();
		}

		[Test]
		public async Task AssignCharacterToUserCommand_Success()
		{
			var mockUserRepository = new Mock<IUserRepository>();
			var mockCharacterRepository = new Mock<ICharacterRepository>();

			mockUserRepository
				.Setup(repo => repo.FindAsync(userId, It.IsAny<ISpecification<User>>(), It.IsAny<CancellationToken>()))
				.ReturnsAsync(owner);

			mockCharacterRepository
				.Setup(repo => repo.FindAsync(characterId, It.IsAny<ISpecification<Character>>(), It.IsAny<CancellationToken>()))
				.ReturnsAsync(character);

			var request = new AssignCharacterToUserCommand(characterId, userId);
			var handler = new AssignCharacterToUserCommandHandler(mockUserRepository.Object, mockCharacterRepository.Object);

			var response = await handler.Handle(request, CancellationToken.None);

			Assert.That(response.Owner.Id, Is.EqualTo(userId));
		}

		[Test]
		public void AssignCharacterToUserCommand_CharacterNotFound()
		{
			var mockUserRepository = new Mock<IUserRepository>();
			var mockCharacterRepository = new Mock<ICharacterRepository>();

			mockUserRepository
				.Setup(repo => repo.FindAsync(userId, It.IsAny<ISpecification<User>>(), It.IsAny<CancellationToken>()))
				.ReturnsAsync(owner);

			var request = new AssignCharacterToUserCommand(characterId, userId);
			var handler = new AssignCharacterToUserCommandHandler(mockUserRepository.Object, mockCharacterRepository.Object);

			Assert.ThatAsync(async () => await handler.Handle(request, CancellationToken.None),
				Throws.TypeOf<BusinessLogicException>()
				.With.Message.EqualTo("Character with specified ID doesn't exist."));
		}

		[Test]
		public void AssignCharacterToUserCommand_UserNotFound()
		{
			var mockUserRepository = new Mock<IUserRepository>();
			var mockCharacterRepository = new Mock<ICharacterRepository>();

			mockCharacterRepository
				.Setup(repo => repo.FindAsync(characterId, It.IsAny<ISpecification<Character>>(), It.IsAny<CancellationToken>()))
				.ReturnsAsync(character);

			var request = new AssignCharacterToUserCommand(characterId, userId);
			var handler = new AssignCharacterToUserCommandHandler(mockUserRepository.Object, mockCharacterRepository.Object);

			Assert.ThatAsync(async () => await handler.Handle(request, CancellationToken.None),
				Throws.TypeOf<BusinessLogicException>()
				.With.Message.EqualTo("User with specified ID doesn't exist."));
		}
	}
}
