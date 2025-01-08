using Moq;
using Tavernkeep.Application.Interfaces;
using Tavernkeep.Application.UseCases.Characters.Commands.EditAbilities;
using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Entities.Pathfinder;
using Tavernkeep.Core.Entities;
using Tavernkeep.Core.Exceptions;
using Tavernkeep.Core.Repositories;
using Tavernkeep.Core.Specifications;
using Tavernkeep.Application.UseCases.Characters.Commands.EditConditions;
using Tavernkeep.Core.Entities.Pathfinder.Conditions;

namespace Tavernkepp.Application.Tests.UseCases.Characters.Commands
{
	internal class EditConditionsCommandTests
	{
		private readonly Guid characterId = Guid.NewGuid();

		private readonly User owner;
		private readonly User master;

		private readonly List<ConditionTemplate> conditions = 
		[
			new() 
			{ 
				Id = "pf2e:condition:blinded",
				Name = "Blinded", 
				HasLevels = false, 
				Modifiers = 
				[
					new() 
					{ 
						Targets = [ModifierTarget.Perception], 
						Value = 4, 
						IsBonus = false 
					}
				] 
			}
		];

		private Character character = default!;


		public EditConditionsCommandTests()
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

			character.Wisdom.Score = 16;
			character.Perception.Proficiency = Proficiency.Trained;
		}

		[Test]
		public async Task EditConditionsCommand_Success()
		{
			var mockUserRepository = new Mock<IUserRepository>();
			var mockCharacterRepository = new Mock<ICharacterRepository>();
			var mockConditionMetadataRepository = new Mock<IConditionMetadataRepository>();
			var mockNotificationService = new Mock<INotificationService>();

			mockUserRepository
				.Setup(repo => repo.FindAsync(owner.Id, It.IsAny<ISpecification<User>>(), It.IsAny<CancellationToken>()!))
				.ReturnsAsync(owner);
			mockCharacterRepository
				.Setup(repo => repo.GetFullCharacterAsync(characterId, It.IsAny<CancellationToken>()))
				.ReturnsAsync(character);
			mockConditionMetadataRepository
				.Setup(repo => repo.GetConditionAsync("Blinded", It.IsAny<CancellationToken>()))
				.ReturnsAsync(conditions[0]);

			int basePerception = character.Perception.Bonus;

			var request = new EditConditionsCommand(owner.Id, characterId, [new() { Name = "Blinded" }]);
			var handler = new EditConditionsCommandHandler(mockUserRepository.Object, mockCharacterRepository.Object, mockConditionMetadataRepository.Object, mockNotificationService.Object);

			await handler.Handle(request, CancellationToken.None);

			Assert.Multiple(() =>
			{
				Assert.That(character.Conditions, Has.Count.EqualTo(1));
				Assert.That(character.Conditions.FirstOrDefault(x => x.Name == "Blinded"), Is.Not.Null);
				Assert.That(character.Perception.Bonus, Is.EqualTo(basePerception - 4));
			});
		}

		[Test]
		public async Task EditConditionsCommand_Success_Master()
		{
			var mockUserRepository = new Mock<IUserRepository>();
			var mockCharacterRepository = new Mock<ICharacterRepository>();
			var mockConditionMetadataRepository = new Mock<IConditionMetadataRepository>();
			var mockNotificationService = new Mock<INotificationService>();

			mockUserRepository
				.Setup(repo => repo.FindAsync(master.Id, It.IsAny<ISpecification<User>>(), It.IsAny<CancellationToken>()))
				.ReturnsAsync(master);
			mockCharacterRepository
				.Setup(repo => repo.GetFullCharacterAsync(characterId, It.IsAny<CancellationToken>()))
				.ReturnsAsync(character);
			mockConditionMetadataRepository
				.Setup(repo => repo.GetConditionAsync("Blinded", It.IsAny<CancellationToken>()))
				.ReturnsAsync(conditions[0]);

			int basePerception = character.Perception.Bonus;

			var request = new EditConditionsCommand(master.Id, characterId, [new() { Name = "Blinded" }]);
			var handler = new EditConditionsCommandHandler(mockUserRepository.Object, mockCharacterRepository.Object, mockConditionMetadataRepository.Object, mockNotificationService.Object);

			await handler.Handle(request, CancellationToken.None);

			Assert.Multiple(() =>
			{
				Assert.That(character.Conditions, Has.Count.EqualTo(1));
				Assert.That(character.Conditions.FirstOrDefault(x => x.Name == "Blinded"), Is.Not.Null);
				Assert.That(character.Perception.Bonus, Is.EqualTo(basePerception - 4));
			});
		}

		[Test]
		public void EditConditionsCommand_InitiatorNotFound()
		{
			var mockUserRepository = new Mock<IUserRepository>();
			var mockCharacterRepository = new Mock<ICharacterRepository>();
			var mockConditionMetadataRepository = new Mock<IConditionMetadataRepository>();
			var mockNotificationService = new Mock<INotificationService>();

			mockCharacterRepository
				.Setup(repo => repo.GetFullCharacterAsync(characterId, It.IsAny<CancellationToken>()))
				.ReturnsAsync(character);

			var request = new EditConditionsCommand(owner.Id, characterId, [new() { Name = "Blinded" }]);
			var handler = new EditConditionsCommandHandler(mockUserRepository.Object, mockCharacterRepository.Object, mockConditionMetadataRepository.Object, mockNotificationService.Object);

			Assert.ThatAsync(async () => await handler.Handle(request, CancellationToken.None),
				Throws.TypeOf<BusinessLogicException>()
				.With.Message.EqualTo("User with specified ID doesn't exist."));
		}

		[Test]
		public void EditConditionsCommand_CharacterNotFound()
		{
			var mockUserRepository = new Mock<IUserRepository>();
			var mockCharacterRepository = new Mock<ICharacterRepository>();
			var mockConditionMetadataRepository = new Mock<IConditionMetadataRepository>();
			var mockNotificationService = new Mock<INotificationService>();

			mockUserRepository
				.Setup(repo => repo.FindAsync(owner.Id, It.IsAny<ISpecification<User>>(), It.IsAny<CancellationToken>()))
				.ReturnsAsync(owner);

			var request = new EditConditionsCommand(owner.Id, characterId, [new() { Name = "Blinded" }]);
			var handler = new EditConditionsCommandHandler(mockUserRepository.Object, mockCharacterRepository.Object, mockConditionMetadataRepository.Object, mockNotificationService.Object);

			Assert.ThatAsync(async () => await handler.Handle(request, CancellationToken.None),
				Throws.TypeOf<BusinessLogicException>()
				.With.Message.EqualTo("Character with specified ID doesn't exist."));
		}

		[Test]
		public void EditConditionsCommand_NotEnoughPermissions()
		{
			var mockUserRepository = new Mock<IUserRepository>();
			var mockCharacterRepository = new Mock<ICharacterRepository>();
			var mockConditionMetadataRepository = new Mock<IConditionMetadataRepository>();
			var mockNotificationService = new Mock<INotificationService>();
			var initiatorId = Guid.NewGuid();

			mockUserRepository
				.Setup(repo => repo.FindAsync(initiatorId, It.IsAny<ISpecification<User>>(), It.IsAny<CancellationToken>()))
				.ReturnsAsync(new User(string.Empty, string.Empty, UserRole.Player));
			mockCharacterRepository
				.Setup(repo => repo.GetFullCharacterAsync(characterId, It.IsAny<CancellationToken>()))
				.ReturnsAsync(character);

			var request = new EditConditionsCommand(initiatorId, characterId, [new() { Name = "Blinded" }]);
			var handler = new EditConditionsCommandHandler(mockUserRepository.Object, mockCharacterRepository.Object, mockConditionMetadataRepository.Object, mockNotificationService.Object);

			Assert.ThatAsync(async () => await handler.Handle(request, CancellationToken.None),
				Throws.TypeOf<InsufficientPermissionException>()
				.With.Message.EqualTo("You do not have the necessary permissions to perform this operation."));
		}
	}
}
