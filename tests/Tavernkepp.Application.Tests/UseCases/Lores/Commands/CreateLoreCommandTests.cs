using Moq;
using Tavernkeep.Application.UseCases.Lores.Commands.CreateLore;
using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Entities;
using Tavernkeep.Core.Entities.Pathfinder;
using Tavernkeep.Core.Entities.Pathfinder.Properties;
using Tavernkeep.Core.Exceptions;
using Tavernkeep.Core.Repositories;
using Tavernkeep.Core.Specifications;

namespace Tavernkepp.Application.Tests.UseCases.Lores.Commands
{
	public class CreateLoreCommandTests
	{
		private readonly string loreTopic = "Academia";
		private readonly Proficiency loreProficiency = Proficiency.Trained;

		private readonly User owner;
		private readonly User master;

		private Character character = default!;

		public CreateLoreCommandTests()
		{
			owner = new User("owner", "owner", UserRole.Player) { Id = Guid.NewGuid() };
			master = new User("master", "master", UserRole.Master) { Id = Guid.NewGuid() };
		}

		[SetUp]
		public void SetUp()
		{
			character = new()
			{
				Id = Guid.NewGuid(),
				Owner = owner,
			};
		}

		[Test]
		public async Task CreateLoreCommand_Success()
		{
			var mockUserRepository = new Mock<IUserRepository>();
			var mockCharacterRepository = new Mock<ICharacterRepository>();

			mockUserRepository
				.Setup(repo => repo.FindAsync(owner.Id, It.IsAny<ISpecification<User>>(), It.IsAny<CancellationToken>()))
				.ReturnsAsync(owner);

			mockCharacterRepository
				.Setup(repo => repo.GetFullCharacterAsync(character.Id, It.IsAny<CancellationToken>()))
				.ReturnsAsync(character);

			var request = new CreateLoreCommand(owner.Id, character.Id, loreTopic, loreProficiency);
			var handler = new CreateLoreCommandHandler(mockUserRepository.Object, mockCharacterRepository.Object);

			var result = await handler.Handle(request, CancellationToken.None);

			Assert.Multiple(() =>
			{
				Assert.That(character.Lores.Any(x => x.Topic == loreTopic), Is.True);
				Assert.That(character.Lores.First(x => x.Topic == loreTopic).Proficiency, Is.EqualTo(loreProficiency));
			});
		}

		[Test]
		public async Task CreateLoreCommand_Success_Master()
		{
			var mockUserRepository = new Mock<IUserRepository>();
			var mockCharacterRepository = new Mock<ICharacterRepository>();

			mockUserRepository
				.Setup(repo => repo.FindAsync(master.Id, It.IsAny<ISpecification<User>>(), It.IsAny<CancellationToken>()))
				.ReturnsAsync(master);

			mockCharacterRepository
				.Setup(repo => repo.GetFullCharacterAsync(character.Id, It.IsAny<CancellationToken>()))
				.ReturnsAsync(character);

			var request = new CreateLoreCommand(master.Id, character.Id, loreTopic, loreProficiency);
			var handler = new CreateLoreCommandHandler(mockUserRepository.Object, mockCharacterRepository.Object);

			var result = await handler.Handle(request, CancellationToken.None);

			Assert.Multiple(() =>
			{
				Assert.That(character.Lores.Any(x => x.Topic == loreTopic), Is.True);
				Assert.That(character.Lores.First(x => x.Topic == loreTopic).Proficiency, Is.EqualTo(loreProficiency));
			});
		}

		[Test]
		public void CreateLoreCommand_InitiatorNotFound()
		{
			var mockUserRepository = new Mock<IUserRepository>();
			var mockCharacterRepository = new Mock<ICharacterRepository>();

			mockCharacterRepository
				.Setup(repo => repo.GetFullCharacterAsync(character.Id, It.IsAny<CancellationToken>()))
				.ReturnsAsync(character);

			var request = new CreateLoreCommand(owner.Id, character.Id, loreTopic, loreProficiency);
			var handler = new CreateLoreCommandHandler(mockUserRepository.Object, mockCharacterRepository.Object);

			Assert.ThatAsync(async () => await handler.Handle(request, CancellationToken.None),
				Throws.TypeOf<BusinessLogicException>()
				.With.Message.EqualTo("User with specified ID doesn't exist."));
		}

		[Test]
		public void CreateLoreCommand_CharacterNotFound()
		{
			var mockUserRepository = new Mock<IUserRepository>();
			var mockCharacterRepository = new Mock<ICharacterRepository>();

			mockUserRepository
				.Setup(repo => repo.FindAsync(owner.Id, It.IsAny<ISpecification<User>>(), It.IsAny<CancellationToken>()))
				.ReturnsAsync(owner);

			var request = new CreateLoreCommand(owner.Id, character.Id, loreTopic, loreProficiency);
			var handler = new CreateLoreCommandHandler(mockUserRepository.Object, mockCharacterRepository.Object);

			Assert.ThatAsync(async () => await handler.Handle(request, CancellationToken.None),
				Throws.TypeOf<BusinessLogicException>()
				.With.Message.EqualTo("Character with specified ID doesn't exist."));
		}

		[Test]
		public void CreateLoreCommand_LoreAlreadyExists()
		{
			var mockUserRepository = new Mock<IUserRepository>();
			var mockCharacterRepository = new Mock<ICharacterRepository>();

			character.Lores.Add(new Lore() { Topic = loreTopic, Proficiency = loreProficiency });

			mockUserRepository
				.Setup(repo => repo.FindAsync(owner.Id, It.IsAny<ISpecification<User>>(), It.IsAny<CancellationToken>()))
				.ReturnsAsync(owner);

			mockCharacterRepository
				.Setup(repo => repo.GetFullCharacterAsync(character.Id, It.IsAny<CancellationToken>()))
				.ReturnsAsync(character);

			var request = new CreateLoreCommand(owner.Id, character.Id, loreTopic, loreProficiency);
			var handler = new CreateLoreCommandHandler(mockUserRepository.Object, mockCharacterRepository.Object);

			Assert.ThatAsync(async () => await handler.Handle(request, CancellationToken.None),
				Throws.TypeOf<BusinessLogicException>()
				.With.Message.EqualTo("Character already has lore skill with this topic."));
		}

		[Test]
		public void CreateLoreCommand_NotEnoughPermissions()
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

			var request = new CreateLoreCommand(initiatorId, character.Id, loreTopic, loreProficiency);
			var handler = new CreateLoreCommandHandler(mockUserRepository.Object, mockCharacterRepository.Object);

			Assert.ThatAsync(async () => await handler.Handle(request, CancellationToken.None),
				Throws.TypeOf<InsufficientPermissionException>()
				.With.Message.EqualTo("You do not have the necessary permissions to perform this operation."));
		}
	}
}
