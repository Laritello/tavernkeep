using Moq;
using Tavernkeep.Application.Interfaces;
using Tavernkeep.Application.UseCases.Custom.Commands.AddCustomSkill;
using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Entities.Pathfinder;
using Tavernkeep.Core.Entities;
using Tavernkeep.Core.Exceptions;
using Tavernkeep.Core.Repositories;
using Tavernkeep.Core.Specifications;
using Tavernkeep.Application.UseCases.Custom.Commands.DeleteCustomSkill;

namespace Tavernkepp.Application.Tests.UseCases.Custom.Commands
{
	internal class DeleteCustomSkillCommandTests
	{
		private readonly Guid characterId = Guid.NewGuid();

		private readonly User owner;
		private readonly User master;

		private Character character = default!;


		public DeleteCustomSkillCommandTests()
		{
			owner = new User("owner", "owner", UserRole.Player) { Id = Guid.NewGuid() };
			master = new User("master", "master", UserRole.Master) { Id = Guid.NewGuid() };
		}

		[SetUp]
		public void SetUp()
		{
			character = CharacterGenerator.Generate(characterId, owner);
			character.Skills.Add(new("Custom", Proficiency.Untrained, SkillType.Custom)
			{
				Owner = character,
				Ability = character.Abilities["Intelligence"]
			});
		}

		[Test]
		public async Task DeleteCustomSkillCommand_Success()
		{
			var mockUserRepository = new Mock<IUserRepository>();
			var mockCharacterRepository = new Mock<ICharacterRepository>();
			var mockNotificationService = new Mock<INotificationService>();

			int skillsAmount = character.Skills.Count;

			mockUserRepository
				.Setup(repo => repo.FindAsync(owner.Id, It.IsAny<ISpecification<User>>(), It.IsAny<CancellationToken>()!))
				.ReturnsAsync(owner);
			mockCharacterRepository
				.Setup(repo => repo.GetFullCharacterAsync(characterId, It.IsAny<CancellationToken>()))
				.ReturnsAsync(character);

			var request = new DeleteCustomSkillCommand(owner.Id, characterId, "Custom");
			var handler = new DeleteCustomSkillCommandHandler(mockUserRepository.Object, mockCharacterRepository.Object, mockNotificationService.Object);

			await handler.Handle(request, CancellationToken.None);

			Assert.Multiple(() =>
			{
				Assert.That(character.Skills, Has.Count.EqualTo(skillsAmount - 1));
				Assert.That(character.Skills.Any(x => x.Name == "Custom"), Is.False);
			});
		}

		[Test]
		public async Task DeleteCustomSkillCommand_Success_Master()
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

			int skillsAmount = character.Skills.Count;

			var request = new DeleteCustomSkillCommand(master.Id, characterId, "Custom");
			var handler = new DeleteCustomSkillCommandHandler(mockUserRepository.Object, mockCharacterRepository.Object, mockNotificationService.Object);

			await handler.Handle(request, CancellationToken.None);

			Assert.Multiple(() =>
			{
				Assert.That(character.Skills, Has.Count.EqualTo(skillsAmount - 1));
				Assert.That(character.Skills.Any(x => x.Name == "Custom"), Is.False);
			});
		}

		[Test]
		public void DeleteCustomSkillCommand_InitiatorNotFound()
		{
			var mockUserRepository = new Mock<IUserRepository>();
			var mockCharacterRepository = new Mock<ICharacterRepository>();
			var mockConditionMetadataRepository = new Mock<IConditionMetadataRepository>();
			var mockNotificationService = new Mock<INotificationService>();

			int skillsAmount = character.Skills.Count;

			mockCharacterRepository
				.Setup(repo => repo.GetFullCharacterAsync(characterId, It.IsAny<CancellationToken>()))
				.ReturnsAsync(character);

			var request = new DeleteCustomSkillCommand(owner.Id, characterId, "Custom");
			var handler = new DeleteCustomSkillCommandHandler(mockUserRepository.Object, mockCharacterRepository.Object, mockNotificationService.Object);

			Assert.ThatAsync(async () => await handler.Handle(request, CancellationToken.None),
				Throws.TypeOf<BusinessLogicException>()
				.With.Message.EqualTo("User with specified ID doesn't exist."));
		}

		[Test]
		public void DeleteCustomSkillCommand_CharacterNotFound()
		{
			var mockUserRepository = new Mock<IUserRepository>();
			var mockCharacterRepository = new Mock<ICharacterRepository>();
			var mockConditionMetadataRepository = new Mock<IConditionMetadataRepository>();
			var mockNotificationService = new Mock<INotificationService>();

			mockUserRepository
				.Setup(repo => repo.FindAsync(owner.Id, It.IsAny<ISpecification<User>>(), It.IsAny<CancellationToken>()))
				.ReturnsAsync(owner);

			var request = new DeleteCustomSkillCommand(owner.Id, characterId, "Custom");
			var handler = new DeleteCustomSkillCommandHandler(mockUserRepository.Object, mockCharacterRepository.Object, mockNotificationService.Object);

			Assert.ThatAsync(async () => await handler.Handle(request, CancellationToken.None),
				Throws.TypeOf<BusinessLogicException>()
				.With.Message.EqualTo("Character with specified ID doesn't exist."));
		}

		[Test]
		public void DeleteCustomSkillCommand_NotEnoughPermissions()
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

			var request = new DeleteCustomSkillCommand(initiatorId, characterId, "Custom");
			var handler = new DeleteCustomSkillCommandHandler(mockUserRepository.Object, mockCharacterRepository.Object, mockNotificationService.Object);

			Assert.ThatAsync(async () => await handler.Handle(request, CancellationToken.None),
				Throws.TypeOf<InsufficientPermissionException>()
				.With.Message.EqualTo("You do not have the necessary permissions to perform this operation."));
		}

		[Test]
		public void DeleteCustomSkillCommand_NoSkill()
		{
			var mockUserRepository = new Mock<IUserRepository>();
			var mockCharacterRepository = new Mock<ICharacterRepository>();
			var mockConditionMetadataRepository = new Mock<IConditionMetadataRepository>();
			var mockNotificationService = new Mock<INotificationService>();
			var initiatorId = Guid.NewGuid();

			mockUserRepository
				.Setup(repo => repo.FindAsync(owner.Id, It.IsAny<ISpecification<User>>(), It.IsAny<CancellationToken>()!))
				.ReturnsAsync(owner);
			mockCharacterRepository
				.Setup(repo => repo.GetFullCharacterAsync(characterId, It.IsAny<CancellationToken>()))
				.ReturnsAsync(character);

			var request = new DeleteCustomSkillCommand(owner.Id, characterId, "Wrong");
			var handler = new DeleteCustomSkillCommandHandler(mockUserRepository.Object, mockCharacterRepository.Object, mockNotificationService.Object);

			Assert.ThatAsync(async () => await handler.Handle(request, CancellationToken.None),
				Throws.TypeOf<BusinessLogicException>()
				.With.Message.EqualTo("Character does not have a skill with this name."));
		}

		[Test]
		public void DeleteCustomSkillCommand_InvalidType()
		{
			var mockUserRepository = new Mock<IUserRepository>();
			var mockCharacterRepository = new Mock<ICharacterRepository>();
			var mockConditionMetadataRepository = new Mock<IConditionMetadataRepository>();
			var mockNotificationService = new Mock<INotificationService>();
			var initiatorId = Guid.NewGuid();

			mockUserRepository
				.Setup(repo => repo.FindAsync(owner.Id, It.IsAny<ISpecification<User>>(), It.IsAny<CancellationToken>()!))
				.ReturnsAsync(owner);
			mockCharacterRepository
				.Setup(repo => repo.GetFullCharacterAsync(characterId, It.IsAny<CancellationToken>()))
				.ReturnsAsync(character);

			var request = new DeleteCustomSkillCommand(owner.Id, characterId, "Arcana");
			var handler = new DeleteCustomSkillCommandHandler(mockUserRepository.Object, mockCharacterRepository.Object, mockNotificationService.Object);

			Assert.ThatAsync(async () => await handler.Handle(request, CancellationToken.None),
				Throws.TypeOf<BusinessLogicException>()
				.With.Message.EqualTo("Deteled skill must either have custom or lore type."));
		}
	}
}
