using Moq;
using Tavernkeep.Application.UseCases.Characters.Commands.DeleteCharacter;
using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Entities;
using Tavernkeep.Core.Entities.Pathfinder;
using Tavernkeep.Core.Exceptions;
using Tavernkeep.Core.Repositories;
using Tavernkeep.Core.Specifications;

namespace Tavernkepp.Application.Tests.UseCases.Characters.Commands
{
	public class DeleteCharacterCommandTests
	{
		private readonly Guid characterId = Guid.NewGuid();

		private readonly User owner;
		private readonly User master;

		private Character character;

		public DeleteCharacterCommandTests()
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
		public async Task DeleteCharacterCommand_Success()
		{
			var mockUserRepository = new Mock<IUserRepository>();
			var mockCharacterRepository = new Mock<ICharacterRepository>();

			mockUserRepository
				.Setup(repo => repo.FindAsync(owner.Id, It.IsAny<ISpecification<User>>(), It.IsAny<CancellationToken>()))
				.ReturnsAsync(owner);

			mockCharacterRepository
				.Setup(repo => repo.FindAsync(characterId, It.IsAny<ISpecification<Character>>(), It.IsAny<CancellationToken>()))
				.ReturnsAsync(character);

			var request = new DeleteCharacterCommand(owner.Id, characterId);
			var handler = new DeleteCharacterCommandHandler(mockUserRepository.Object, mockCharacterRepository.Object);

			await handler.Handle(request, CancellationToken.None);
		}

		[Test]
		public async Task DeleteCharacterCommand_Success_Master()
		{
			var mockUserRepository = new Mock<IUserRepository>();
			var mockCharacterRepository = new Mock<ICharacterRepository>();

			mockUserRepository
				.Setup(repo => repo.FindAsync(master.Id, It.IsAny<ISpecification<User>>(), It.IsAny<CancellationToken>()))
				.ReturnsAsync(master);

			mockCharacterRepository
				.Setup(repo => repo.FindAsync(characterId, It.IsAny<ISpecification<Character>>(), It.IsAny<CancellationToken>()))
				.ReturnsAsync(character);

			var request = new DeleteCharacterCommand(master.Id, characterId);
			var handler = new DeleteCharacterCommandHandler(mockUserRepository.Object, mockCharacterRepository.Object);

			await handler.Handle(request, CancellationToken.None);
		}

		[Test]
		public void DeleteCharacterCommand_InitiatorNotFound()
		{
			var mockUserRepository = new Mock<IUserRepository>();
			var mockCharacterRepository = new Mock<ICharacterRepository>();

			mockCharacterRepository
				.Setup(repo => repo.FindAsync(characterId, It.IsAny<ISpecification<Character>>(), It.IsAny<CancellationToken>()))
				.ReturnsAsync(character);

			var request = new DeleteCharacterCommand(owner.Id, characterId);
			var handler = new DeleteCharacterCommandHandler(mockUserRepository.Object, mockCharacterRepository.Object);

			Assert.ThatAsync(async () => await handler.Handle(request, CancellationToken.None),
				Throws.TypeOf<BusinessLogicException>()
				.With.Message.EqualTo("User with specified ID doesn't exist."));
		}

		[Test]
		public void DeleteCharacterCommand_CharacterNotFound()
		{
			var mockUserRepository = new Mock<IUserRepository>();
			var mockCharacterRepository = new Mock<ICharacterRepository>();

			mockUserRepository
				.Setup(repo => repo.FindAsync(owner.Id, It.IsAny<ISpecification<User>>(), It.IsAny<CancellationToken>()))
				.ReturnsAsync(owner);

			var request = new DeleteCharacterCommand(owner.Id, characterId);
			var handler = new DeleteCharacterCommandHandler(mockUserRepository.Object, mockCharacterRepository.Object);

			Assert.ThatAsync(async () => await handler.Handle(request, CancellationToken.None),
				Throws.TypeOf<BusinessLogicException>()
				.With.Message.EqualTo("Character with specified ID doesn't exist."));
		}

		[Test]
		public void DeleteCharacterCommand_NotEnoughPermissions()
		{
			var mockUserRepository = new Mock<IUserRepository>();
			var mockCharacterRepository = new Mock<ICharacterRepository>();
			var initiatorId = Guid.NewGuid();

			mockUserRepository
				.Setup(repo => repo.FindAsync(initiatorId, It.IsAny<ISpecification<User>>(), It.IsAny<CancellationToken>()))
				.ReturnsAsync(new User(string.Empty, string.Empty, UserRole.Player));

			mockCharacterRepository
				.Setup(repo => repo.FindAsync(characterId, It.IsAny<ISpecification<Character>>(), It.IsAny<CancellationToken>()))
				.ReturnsAsync(character);

			var request = new DeleteCharacterCommand(initiatorId, characterId);
			var handler = new DeleteCharacterCommandHandler(mockUserRepository.Object, mockCharacterRepository.Object);

			Assert.ThatAsync(async () => await handler.Handle(request, CancellationToken.None),
				Throws.TypeOf<InsufficientPermissionException>()
				.With.Message.EqualTo("You do not have the necessary permissions to perform this operation."));
		}
	}
}
