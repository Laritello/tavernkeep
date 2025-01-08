using Moq;
using Tavernkeep.Application.UseCases.Lores.Commands.DeleteLore;
using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Entities;
using Tavernkeep.Core.Entities.Pathfinder;
using Tavernkeep.Core.Entities.Pathfinder.Properties;
using Tavernkeep.Core.Exceptions;
using Tavernkeep.Core.Repositories;
using Tavernkeep.Core.Specifications;

namespace Tavernkepp.Application.Tests.UseCases.Lores.Commands
{
	public class DeleteLoreCommandTests
	{
		private readonly string loreTopic = "Academia";

		private readonly User owner;
		private readonly User master;
		private readonly Lore lore;

		private Character character = default!;

		public DeleteLoreCommandTests()
		{
			owner = new User("owner", "owner", UserRole.Player) { Id = Guid.NewGuid() };
			master = new User("master", "master", UserRole.Master) { Id = Guid.NewGuid() };
			lore = new Lore()
			{
				Proficiency = Proficiency.Trained,
				Topic = loreTopic,
			};
		}

		[SetUp]
		public void SetUp()
		{
			character = new()
			{
				Id = Guid.NewGuid(),
				Owner = owner,
				Lores = [lore],
			};
		}

		[Test]
		public async Task DeleteLoreCommand_Success()
		{
			var mockUserRepository = new Mock<IUserRepository>();
			var mockCharacterRepository = new Mock<ICharacterRepository>();

			mockUserRepository
				.Setup(repo => repo.FindAsync(owner.Id, It.IsAny<ISpecification<User>>(), It.IsAny<CancellationToken>()))
				.ReturnsAsync(owner);

			mockCharacterRepository
				.Setup(repo => repo.GetFullCharacterAsync(character.Id, It.IsAny<CancellationToken>()))
				.ReturnsAsync(character);

			var request = new DeleteLoreCommand(owner.Id, character.Id, loreTopic);
			var handler = new DeleteLoreCommandHandler(mockUserRepository.Object, mockCharacterRepository.Object);

			await handler.Handle(request, CancellationToken.None);
		}

		[Test]
		public async Task DeleteLoreCommand_Success_Master()
		{
			var mockUserRepository = new Mock<IUserRepository>();
			var mockCharacterRepository = new Mock<ICharacterRepository>();

			mockUserRepository
				.Setup(repo => repo.FindAsync(master.Id, It.IsAny<ISpecification<User>>(), It.IsAny<CancellationToken>()))
				.ReturnsAsync(master);

			mockCharacterRepository
				.Setup(repo => repo.GetFullCharacterAsync(character.Id, It.IsAny<CancellationToken>()))
				.ReturnsAsync(character);

			var request = new DeleteLoreCommand(master.Id, character.Id, loreTopic);
			var handler = new DeleteLoreCommandHandler(mockUserRepository.Object, mockCharacterRepository.Object);

			await handler.Handle(request, CancellationToken.None);
		}

		[Test]
		public void DeleteLoreCommand_InitiatorNotFound()
		{
			var mockUserRepository = new Mock<IUserRepository>();
			var mockCharacterRepository = new Mock<ICharacterRepository>();

			mockCharacterRepository
				.Setup(repo => repo.GetFullCharacterAsync(character.Id, It.IsAny<CancellationToken>()))
				.ReturnsAsync(character);

			var request = new DeleteLoreCommand(owner.Id, character.Id, loreTopic);
			var handler = new DeleteLoreCommandHandler(mockUserRepository.Object, mockCharacterRepository.Object);

			Assert.ThatAsync(async () => await handler.Handle(request, CancellationToken.None),
				Throws.TypeOf<BusinessLogicException>()
				.With.Message.EqualTo("User with specified ID doesn't exist."));
		}

		[Test]
		public void DeleteLoreCommand_CharacterNotFound()
		{
			var mockUserRepository = new Mock<IUserRepository>();
			var mockCharacterRepository = new Mock<ICharacterRepository>();

			mockUserRepository
				.Setup(repo => repo.FindAsync(owner.Id, It.IsAny<ISpecification<User>>(), It.IsAny<CancellationToken>()))
				.ReturnsAsync(owner);

			var request = new DeleteLoreCommand(owner.Id, character.Id, loreTopic);
			var handler = new DeleteLoreCommandHandler(mockUserRepository.Object, mockCharacterRepository.Object);

			Assert.ThatAsync(async () => await handler.Handle(request, CancellationToken.None),
				Throws.TypeOf<BusinessLogicException>()
				.With.Message.EqualTo("Character with specified ID doesn't exist."));
		}

		[Test]
		public void DeleteLoreCommand_LoreNotFound()
		{
			var mockUserRepository = new Mock<IUserRepository>();
			var mockCharacterRepository = new Mock<ICharacterRepository>();

			character.Lores.Clear();

			mockUserRepository
				.Setup(repo => repo.FindAsync(owner.Id, It.IsAny<ISpecification<User>>(), It.IsAny<CancellationToken>()))
				.ReturnsAsync(owner);

			mockCharacterRepository
				.Setup(repo => repo.GetFullCharacterAsync(character.Id, It.IsAny<CancellationToken>()))
				.ReturnsAsync(character);

			var request = new DeleteLoreCommand(owner.Id, character.Id, loreTopic);
			var handler = new DeleteLoreCommandHandler(mockUserRepository.Object, mockCharacterRepository.Object);

			Assert.ThatAsync(async () => await handler.Handle(request, CancellationToken.None),
				Throws.TypeOf<BusinessLogicException>()
				.With.Message.EqualTo("Character does not have lore skill with this topic."));
		}

		[Test]
		public void DeleteLoreCommand_NotEnoughPermissions()
		{
			var mockUserRepository = new Mock<IUserRepository>();
			var mockCharacterRepository = new Mock<ICharacterRepository>();
			var initiatorId = Guid.NewGuid();

			mockUserRepository
				.Setup(repo => repo.FindAsync(initiatorId, It.IsAny<ISpecification<User>>(), It.IsAny<CancellationToken>()))
				.ReturnsAsync(new User(string.Empty, string.Empty, UserRole.Player));

			mockCharacterRepository
				.Setup(repo => repo.GetFullCharacterAsync(character.Id, It.IsAny<CancellationToken>()))
				.ReturnsAsync(character);

			var request = new DeleteLoreCommand(initiatorId, character.Id, loreTopic);
			var handler = new DeleteLoreCommandHandler(mockUserRepository.Object, mockCharacterRepository.Object);

			Assert.ThatAsync(async () => await handler.Handle(request, CancellationToken.None),
				Throws.TypeOf<InsufficientPermissionException>()
				.With.Message.EqualTo("You do not have the necessary permissions to perform this operation."));
		}
	}
}
