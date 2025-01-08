using Moq;
using System.Text.Json;
using Tavernkeep.Application.Interfaces;
using Tavernkeep.Application.UseCases.Conditions.Commands.ApplyCondition;
using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Entities;
using Tavernkeep.Core.Entities.Pathfinder;
using Tavernkeep.Core.Entities.Pathfinder.Conditions;
using Tavernkeep.Core.Exceptions;
using Tavernkeep.Core.Repositories;
using Tavernkeep.Core.Specifications;
using Tavernkepp.Application.Tests.Utility;

namespace Tavernkepp.Application.Tests.UseCases.Conditions.Commands
{
	public class ApplyConditionCommandTests
	{
		private readonly Guid characterId = Guid.NewGuid();
		private readonly string conditionName = "Clumsy";

		private Character character;

		private readonly Dictionary<string, ConditionTemplate> conditions;

		private readonly User owner;
		private readonly User master;

		public ApplyConditionCommandTests()
		{
			owner = new User("owner", "owner", UserRole.Player) { Id = Guid.NewGuid() };
			master = new User("master", "master", UserRole.Master) { Id = Guid.NewGuid() };

			conditions = PopulateConditions("en-UK");
			character = TempGenerator.GenerateCharacter(characterId);
		}

		private static Dictionary<string, ConditionTemplate> PopulateConditions(string culture)
		{
			using var sr = new StreamReader($"Resources/Conditions.{culture}.json");

			var json = sr.ReadToEnd();
			var conditions = JsonSerializer.Deserialize<List<ConditionTemplate>>(json) ?? [];

			return conditions.ToDictionary(c => c.Name);
		}

		[SetUp]
		public void SetUp()
		{
			character = TempGenerator.GenerateCharacter(characterId);
			character.Owner = owner;
		}

		[Test]
		public async Task ApplyConditionCommand_Success()
		{
			var mockUserRepository = new Mock<IUserRepository>();
			var mockCharacterRepository = new Mock<ICharacterRepository>();
			var mockConditionRepository = new Mock<IConditionMetadataRepository>();
			var mockNotificationService = new Mock<INotificationService>();
			var acrobaticsOld = character.Acrobatics.Bonus;

			mockUserRepository
				.Setup(repo => repo.FindAsync(owner.Id, It.IsAny<ISpecification<User>>(), It.IsAny<CancellationToken>()))
				.ReturnsAsync(owner);
			mockCharacterRepository
				.Setup(repo => repo.GetFullCharacterAsync(characterId, It.IsAny<CancellationToken>()))
				.ReturnsAsync(character);
			mockConditionRepository
				.Setup(repo => repo.GetConditionAsync(conditionName, It.IsAny<CancellationToken>()))
				.ReturnsAsync(conditions[conditionName]);

			var request = new ApplyConditionCommand(owner.Id, characterId, conditionName, 1);
			var handler = new ApplyConditionCommandHandler(mockUserRepository.Object, mockCharacterRepository.Object, mockConditionRepository.Object, mockNotificationService.Object);

			var response = await handler.Handle(request, CancellationToken.None);

			Assert.That(response.Acrobatics.Bonus, Is.EqualTo(acrobaticsOld - 1));
		}

		[Test]
		public async Task ApplyConditionCommand_Success_Master()
		{
			var mockUserRepository = new Mock<IUserRepository>();
			var mockCharacterRepository = new Mock<ICharacterRepository>();
			var mockConditionRepository = new Mock<IConditionMetadataRepository>();
			var mockNotificationService = new Mock<INotificationService>();
			var acrobaticsOld = character.Acrobatics.Bonus;

			mockUserRepository
				.Setup(repo => repo.FindAsync(master.Id, It.IsAny<ISpecification<User>>(), It.IsAny<CancellationToken>()))
				.ReturnsAsync(master);
			mockCharacterRepository
				.Setup(repo => repo.GetFullCharacterAsync(characterId, It.IsAny<CancellationToken>()))
				.ReturnsAsync(character);
			mockConditionRepository
				.Setup(repo => repo.GetConditionAsync(conditionName, It.IsAny<CancellationToken>()))
				.ReturnsAsync(conditions[conditionName]);

			var request = new ApplyConditionCommand(master.Id, characterId, conditionName, 1);
			var handler = new ApplyConditionCommandHandler(mockUserRepository.Object, mockCharacterRepository.Object, mockConditionRepository.Object, mockNotificationService.Object);

			var response = await handler.Handle(request, CancellationToken.None);

			Assert.That(response.Acrobatics.Bonus, Is.EqualTo(acrobaticsOld - 1));
		}

		[Test]
		public void ApplyConditionCommand_InitiatorNotFound()
		{
			var mockUserRepository = new Mock<IUserRepository>();
			var mockCharacterRepository = new Mock<ICharacterRepository>();
			var mockConditionRepository = new Mock<IConditionMetadataRepository>();
			var mockNotificationService = new Mock<INotificationService>();
			var acrobaticsOld = character.Acrobatics.Bonus;

			mockCharacterRepository
				.Setup(repo => repo.GetFullCharacterAsync(characterId, It.IsAny<CancellationToken>()))
				.ReturnsAsync(character);
			mockConditionRepository
				.Setup(repo => repo.GetConditionAsync(conditionName, It.IsAny<CancellationToken>()))
				.ReturnsAsync(conditions[conditionName]);

			var request = new ApplyConditionCommand(owner.Id, characterId, conditionName, 1);
			var handler = new ApplyConditionCommandHandler(mockUserRepository.Object, mockCharacterRepository.Object, mockConditionRepository.Object, mockNotificationService.Object);

			Assert.ThatAsync(async () => await handler.Handle(request, CancellationToken.None),
				Throws.TypeOf<BusinessLogicException>()
				.With.Message.EqualTo("User with specified ID doesn't exist."));
		}

		[Test]
		public void ApplyConditionCommand_CharacterNotFound()
		{
			var mockUserRepository = new Mock<IUserRepository>();
			var mockCharacterRepository = new Mock<ICharacterRepository>();
			var mockConditionRepository = new Mock<IConditionMetadataRepository>();
			var mockNotificationService = new Mock<INotificationService>();
			var acrobaticsOld = character.Acrobatics.Bonus;

			mockUserRepository
				.Setup(repo => repo.FindAsync(owner.Id, It.IsAny<ISpecification<User>>(), It.IsAny<CancellationToken>()))
				.ReturnsAsync(owner);
			mockConditionRepository
				.Setup(repo => repo.GetConditionAsync(conditionName, It.IsAny<CancellationToken>()))
				.ReturnsAsync(conditions[conditionName]);

			var request = new ApplyConditionCommand(owner.Id, characterId, conditionName, 1);
			var handler = new ApplyConditionCommandHandler(mockUserRepository.Object, mockCharacterRepository.Object, mockConditionRepository.Object, mockNotificationService.Object);

			Assert.ThatAsync(async () => await handler.Handle(request, CancellationToken.None),
				Throws.TypeOf<BusinessLogicException>()
				.With.Message.EqualTo("Character with specified ID doesn't exist."));
		}

		[Test]
		public void ApplyConditionCommand_NotEnoughPermissions()
		{
			var mockUserRepository = new Mock<IUserRepository>();
			var mockCharacterRepository = new Mock<ICharacterRepository>();
			var mockConditionRepository = new Mock<IConditionMetadataRepository>();
			var mockNotificationService = new Mock<INotificationService>();
			var acrobaticsOld = character.Acrobatics.Bonus;
			var initiatorId = Guid.NewGuid();

			mockUserRepository
				.Setup(repo => repo.FindAsync(initiatorId, It.IsAny<ISpecification<User>>(), It.IsAny<CancellationToken>()))
				.ReturnsAsync(new User(string.Empty, string.Empty, UserRole.Player));
			mockCharacterRepository
				.Setup(repo => repo.GetFullCharacterAsync(characterId, It.IsAny<CancellationToken>()))
				.ReturnsAsync(character);

			var request = new ApplyConditionCommand(initiatorId, characterId, conditionName, 1);
			var handler = new ApplyConditionCommandHandler(mockUserRepository.Object, mockCharacterRepository.Object, mockConditionRepository.Object, mockNotificationService.Object);

			Assert.ThatAsync(async () => await handler.Handle(request, CancellationToken.None),
				Throws.TypeOf<InsufficientPermissionException>()
				.With.Message.EqualTo("You do not have the necessary permissions to perform this operation."));
		}

		[Test]
		public void ApplyConditionCommand_ConditionNotFound()
		{
			var mockUserRepository = new Mock<IUserRepository>();
			var mockCharacterRepository = new Mock<ICharacterRepository>();
			var mockConditionRepository = new Mock<IConditionMetadataRepository>();
			var mockNotificationService = new Mock<INotificationService>();
			var acrobaticsOld = character.Acrobatics.Bonus;

			mockUserRepository
				.Setup(repo => repo.FindAsync(owner.Id, It.IsAny<ISpecification<User>>(), It.IsAny<CancellationToken>()))
				.ReturnsAsync(owner);
			mockCharacterRepository
				.Setup(repo => repo.GetFullCharacterAsync(characterId, It.IsAny<CancellationToken>()))
				.ReturnsAsync(character);

			var request = new ApplyConditionCommand(owner.Id, characterId, conditionName, 1);
			var handler = new ApplyConditionCommandHandler(mockUserRepository.Object, mockCharacterRepository.Object, mockConditionRepository.Object, mockNotificationService.Object);

			Assert.ThatAsync(async () => await handler.Handle(request, CancellationToken.None),
				Throws.TypeOf<BusinessLogicException>()
				.With.Message.EqualTo("Condition with specified name doesn't exist."));
		}
	}
}
