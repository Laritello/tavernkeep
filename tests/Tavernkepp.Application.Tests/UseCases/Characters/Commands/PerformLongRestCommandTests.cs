using Moq;
using Tavernkeep.Application.Interfaces;
using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Entities.Pathfinder;
using Tavernkeep.Core.Entities;
using Tavernkeep.Core.Exceptions;
using Tavernkeep.Core.Repositories;
using Tavernkeep.Application.UseCases.Characters.Commands.PerformLongRest;
using Tavernkeep.Core.Specifications;
using Tavernkeep.Core.Entities.Pathfinder.Conditions;

namespace Tavernkepp.Application.Tests.UseCases.Characters.Commands
{
	internal class PerformLongRestCommandTests
	{
		private readonly Guid characterId = Guid.NewGuid();

		private readonly User owner;
		private readonly User master;

		private Character character = default!;

		public PerformLongRestCommandTests()
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
				Owner = owner,
			};
		}

		[Test]
		[TestCase(1, 12, 10)]
		[TestCase(2, 14, 23)]
		[TestCase(3, 16, 32)]
		[TestCase(4, 18, 54)]
		[TestCase(5, 12, 70)]
		[TestCase(6, 16, 82)]
		public async Task PerformLongRestCommand_Success(int level, int constitutionScore, int currentHealth)
		{
			var mockUserRepository = new Mock<IUserRepository>();
			var mockCharacterRepository = new Mock<ICharacterRepository>();
			var mockConditionsRepository = new Mock<IConditionMetadataRepository>();
			var mockNotificationService = new Mock<INotificationService>();

			character.Level = level;
			character.Constitution.Score = constitutionScore;

			character.Health.Temporary = 0;
			character.Health.Max = 100;
			character.Health.Current = currentHealth;

			var calculatedNewHealth = currentHealth + level * ((constitutionScore - 10) / 2);

			mockUserRepository
				.Setup(repo => repo.FindAsync(owner.Id, It.IsAny<ISpecification<User>>(), It.IsAny<CancellationToken>()))
				.ReturnsAsync(owner);
			mockCharacterRepository
				.Setup(repo => repo.GetFullCharacterAsync(characterId, It.IsAny<CancellationToken>()))
				.ReturnsAsync(character);
			

			var request = new PerformLongRestCommand(owner.Id, characterId, false, false);
			var handler = new PerformLongRestCommandHandler(mockUserRepository.Object, mockCharacterRepository.Object, mockConditionsRepository.Object, mockNotificationService.Object);

			await handler.Handle(request, CancellationToken.None);

			Assert.Multiple(() =>
			{
				Assert.That(character.Health.Current, Is.EqualTo(Math.Min(character.Health.Max, calculatedNewHealth)));
			});
		}

		[Test]
		public async Task PerformLongRestCommand_Success_NoComfort()
		{
			var mockUserRepository = new Mock<IUserRepository>();
			var mockCharacterRepository = new Mock<ICharacterRepository>();
			var mockConditionsRepository = new Mock<IConditionMetadataRepository>();
			var mockNotificationService = new Mock<INotificationService>();

			character.Level = 6;
			character.Constitution.Score = 14;

			character.Health.Temporary = 0;
			character.Health.Max = 100;
			character.Health.Current = 18;

			mockUserRepository
				.Setup(repo => repo.FindAsync(owner.Id, It.IsAny<ISpecification<User>>(), It.IsAny<CancellationToken>()))
				.ReturnsAsync(owner);
			mockCharacterRepository
				.Setup(repo => repo.GetFullCharacterAsync(characterId, It.IsAny<CancellationToken>()))
				.ReturnsAsync(character);


			var request = new PerformLongRestCommand(owner.Id, characterId, true, false);
			var handler = new PerformLongRestCommandHandler(mockUserRepository.Object, mockCharacterRepository.Object, mockConditionsRepository.Object, mockNotificationService.Object);

			await handler.Handle(request, CancellationToken.None);

			Assert.Multiple(() =>
			{
				Assert.That(character.Health.Current, Is.EqualTo(24));
			});
		}

		[Test]
		public async Task PerformLongRestCommand_Success_InArmor()
		{
			var mockUserRepository = new Mock<IUserRepository>();
			var mockCharacterRepository = new Mock<ICharacterRepository>();
			var mockConditionsRepository = new Mock<IConditionMetadataRepository>();
			var mockNotificationService = new Mock<INotificationService>();

			character.Level = 6;
			character.Constitution.Score = 14;

			character.Health.Temporary = 0;
			character.Health.Max = 100;
			character.Health.Current = 18;

			mockUserRepository
				.Setup(repo => repo.FindAsync(owner.Id, It.IsAny<ISpecification<User>>(), It.IsAny<CancellationToken>()))
				.ReturnsAsync(owner);
			mockCharacterRepository
				.Setup(repo => repo.GetFullCharacterAsync(characterId, It.IsAny<CancellationToken>()))
				.ReturnsAsync(character);
			mockConditionsRepository
				.Setup(repo => repo.GetConditionAsync("Fatigued", It.IsAny<CancellationToken>()))
				.ReturnsAsync(new ConditionTemplate() { Name = "Fatigued" });

			var request = new PerformLongRestCommand(owner.Id, characterId, false, true);
			var handler = new PerformLongRestCommandHandler(mockUserRepository.Object, mockCharacterRepository.Object, mockConditionsRepository.Object, mockNotificationService.Object);

			await handler.Handle(request, CancellationToken.None);

			Assert.Multiple(() =>
			{
				Assert.That(character.Health.Current, Is.EqualTo(30));
				Assert.That(character.Conditions.Any(c => c.Name == "Fatigued"));
			});
		}

		[Test]
		public async Task PerformLongRestCommand_Success_Master()
		{
			var mockUserRepository = new Mock<IUserRepository>();
			var mockCharacterRepository = new Mock<ICharacterRepository>();
			var mockConditionsRepository = new Mock<IConditionMetadataRepository>();
			var mockNotificationService = new Mock<INotificationService>();

			character.Level = 6;
			character.Constitution.Score = 14;

			character.Health.Temporary = 0;
			character.Health.Max = 100;
			character.Health.Current = 18;

			mockUserRepository
				.Setup(repo => repo.FindAsync(master.Id, It.IsAny<ISpecification<User>>(), It.IsAny<CancellationToken>()))
				.ReturnsAsync(master);
			mockCharacterRepository
				.Setup(repo => repo.GetFullCharacterAsync(characterId, It.IsAny<CancellationToken>()))
				.ReturnsAsync(character);

			var request = new PerformLongRestCommand(master.Id, characterId, false, false);
			var handler = new PerformLongRestCommandHandler(mockUserRepository.Object, mockCharacterRepository.Object, mockConditionsRepository.Object, mockNotificationService.Object);

			await handler.Handle(request, CancellationToken.None);

			Assert.That(character.Health.Current, Is.EqualTo(30));
		}

		[Test]
		public void PerformLongRestCommand_InitiatorNotFound()
		{
			var mockUserRepository = new Mock<IUserRepository>();
			var mockCharacterRepository = new Mock<ICharacterRepository>();
			var mockConditionsRepository = new Mock<IConditionMetadataRepository>();
			var mockNotificationService = new Mock<INotificationService>();

			mockCharacterRepository
				.Setup(repo => repo.GetFullCharacterAsync(characterId, It.IsAny<CancellationToken>()))
				.ReturnsAsync(character);

			var request = new PerformLongRestCommand(owner.Id, characterId, false, false);
			var handler = new PerformLongRestCommandHandler(mockUserRepository.Object, mockCharacterRepository.Object, mockConditionsRepository.Object, mockNotificationService.Object);

			Assert.ThatAsync(async () => await handler.Handle(request, CancellationToken.None),
				Throws.TypeOf<BusinessLogicException>()
				.With.Message.EqualTo("User with specified ID doesn't exist."));
		}

		[Test]
		public void PerformLongRestCommand_CharacterNotFound()
		{
			var mockUserRepository = new Mock<IUserRepository>();
			var mockCharacterRepository = new Mock<ICharacterRepository>();
			var mockConditionsRepository = new Mock<IConditionMetadataRepository>();
			var mockNotificationService = new Mock<INotificationService>();

			mockUserRepository
				.Setup(repo => repo.FindAsync(owner.Id, It.IsAny<ISpecification<User>>(), It.IsAny<CancellationToken>()))
				.ReturnsAsync(owner);

			var request = new PerformLongRestCommand(owner.Id, characterId, false, false);
			var handler = new PerformLongRestCommandHandler(mockUserRepository.Object, mockCharacterRepository.Object, mockConditionsRepository.Object, mockNotificationService.Object);

			Assert.ThatAsync(async () => await handler.Handle(request, CancellationToken.None),
				Throws.TypeOf<BusinessLogicException>()
				.With.Message.EqualTo("Character with specified ID doesn't exist."));
		}

		[Test]
		public void PerformLongRestCommand_NotEnoughPermissions()
		{
			var mockUserRepository = new Mock<IUserRepository>();
			var mockCharacterRepository = new Mock<ICharacterRepository>();
			var mockConditionsRepository = new Mock<IConditionMetadataRepository>();
			var mockNotificationService = new Mock<INotificationService>();
			var initiatorId = Guid.NewGuid();

			mockUserRepository
				.Setup(repo => repo.FindAsync(initiatorId, It.IsAny<ISpecification<User>>(), It.IsAny<CancellationToken>()))
				.ReturnsAsync(new User(string.Empty, string.Empty, UserRole.Player));
			mockCharacterRepository
				.Setup(repo => repo.GetFullCharacterAsync(characterId, It.IsAny<CancellationToken>()))
				.ReturnsAsync(character);

			var request = new PerformLongRestCommand(initiatorId, characterId, false, false);
			var handler = new PerformLongRestCommandHandler(mockUserRepository.Object, mockCharacterRepository.Object, mockConditionsRepository.Object, mockNotificationService.Object);

			Assert.ThatAsync(async () => await handler.Handle(request, CancellationToken.None),
				Throws.TypeOf<InsufficientPermissionException>()
				.With.Message.EqualTo("You do not have the necessary permissions to perform this operation."));
		}
	}
}
