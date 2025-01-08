using Moq;
using Tavernkeep.Application.Interfaces;
using Tavernkeep.Application.UseCases.Characters.Commands.EditSkills;
using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Entities;
using Tavernkeep.Core.Entities.Pathfinder;
using Tavernkeep.Core.Exceptions;
using Tavernkeep.Core.Repositories;
using Tavernkeep.Core.Specifications;

namespace Tavernkepp.Application.Tests.UseCases.Characters.Commands
{
	public class EditSkillsCommandTests
	{
		private readonly Guid characterId = Guid.NewGuid();
		private readonly Proficiency proficiency = Proficiency.Trained;

		private readonly User owner;
		private readonly User master;

		private Character character = default!;

		public EditSkillsCommandTests()
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
		public async Task EditSkillsCommand_Success()
		{
			var mockUserRepository = new Mock<IUserRepository>();
			var mockCharacterRepository = new Mock<ICharacterRepository>();
			var mockNotificationService = new Mock<INotificationService>();

			mockUserRepository
				.Setup(repo => repo.FindAsync(owner.Id, It.IsAny<ISpecification<User>>(), It.IsAny<CancellationToken>()))
				.ReturnsAsync(owner);
			mockCharacterRepository
				.Setup(repo => repo.GetFullCharacterAsync(characterId, It.IsAny<CancellationToken>()))
				.ReturnsAsync(character);

			var request = new EditSkillsCommand(owner.Id, characterId, new() { { SkillType.Acrobatics, proficiency } });
			var handler = new EditSkillsCommandHandler(mockUserRepository.Object, mockCharacterRepository.Object, mockNotificationService.Object);

			await handler.Handle(request, CancellationToken.None);

			Assert.That(character.Acrobatics.Proficiency, Is.EqualTo(proficiency));
		}

		[Test]
		[TestCase(SkillType.Acrobatics)]
		[TestCase(SkillType.Arcana)]
		[TestCase(SkillType.Athletics)]
		[TestCase(SkillType.Crafting)]
		[TestCase(SkillType.Deception)]
		[TestCase(SkillType.Diplomacy)]
		[TestCase(SkillType.Intimidation)]
		[TestCase(SkillType.Medicine)]
		[TestCase(SkillType.Nature)]
		[TestCase(SkillType.Occultism)]
		[TestCase(SkillType.Performance)]
		[TestCase(SkillType.Religion)]
		[TestCase(SkillType.Society)]
		[TestCase(SkillType.Stealth)]
		[TestCase(SkillType.Survival)]
		[TestCase(SkillType.Thievery)]
		public async Task EditSkillsCommand_Success_Master(SkillType type)
		{
			var mockUserRepository = new Mock<IUserRepository>();
			var mockCharacterRepository = new Mock<ICharacterRepository>();
			var mockNotificationService = new Mock<INotificationService>();

			mockUserRepository
				.Setup(repo => repo.FindAsync(master.Id, It.IsAny<ISpecification<User>>(), It.IsAny<CancellationToken>()))
				.ReturnsAsync(master);
			mockCharacterRepository
				.Setup(repo => repo.GetFullCharacterAsync(characterId, It.IsAny<CancellationToken>()))
				.ReturnsAsync(character);

			var request = new EditSkillsCommand(master.Id, characterId, new() { { type, proficiency } });
			var handler = new EditSkillsCommandHandler(mockUserRepository.Object, mockCharacterRepository.Object, mockNotificationService.Object);

			await handler.Handle(request, CancellationToken.None);

			Assert.That(character.GetSkill(type).Proficiency, Is.EqualTo(proficiency));
		}

		[Test]
		public void EditSkillsCommand_InitiatorNotFound()
		{
			var mockUserRepository = new Mock<IUserRepository>();
			var mockCharacterRepository = new Mock<ICharacterRepository>();
			var mockNotificationService = new Mock<INotificationService>();

			mockCharacterRepository
				.Setup(repo => repo.GetFullCharacterAsync(characterId, It.IsAny<CancellationToken>()))
				.ReturnsAsync(character);

			var request = new EditSkillsCommand(owner.Id, characterId, new() { { SkillType.Acrobatics, proficiency } });
			var handler = new EditSkillsCommandHandler(mockUserRepository.Object, mockCharacterRepository.Object, mockNotificationService.Object);

			var ex = Assert.ThrowsAsync<BusinessLogicException>(async () => await handler.Handle(request, CancellationToken.None));
			Assert.That(ex.Message, Is.EqualTo("User with specified ID doesn't exist."));
		}

		[Test]
		public void EditSkillsCommand_CharacterNotFound()
		{
			var mockUserRepository = new Mock<IUserRepository>();
			var mockCharacterRepository = new Mock<ICharacterRepository>();
			var mockNotificationService = new Mock<INotificationService>();

			mockUserRepository
				.Setup(repo => repo.FindAsync(owner.Id, It.IsAny<ISpecification<User>>(), It.IsAny<CancellationToken>()))
				.ReturnsAsync(owner);

			var request = new EditSkillsCommand(owner.Id, characterId, new() { { SkillType.Acrobatics, proficiency } });
			var handler = new EditSkillsCommandHandler(mockUserRepository.Object, mockCharacterRepository.Object, mockNotificationService.Object);

			var ex = Assert.ThrowsAsync<BusinessLogicException>(async () => await handler.Handle(request, CancellationToken.None));
			Assert.That(ex.Message, Is.EqualTo("Character with specified ID doesn't exist."));
		}

		[Test]
		public void EditSkillsCommand_NotEnoughPermissions()
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

			var request = new EditSkillsCommand(initiatorId, characterId, new() { { SkillType.Acrobatics, proficiency } });
			var handler = new EditSkillsCommandHandler(mockUserRepository.Object, mockCharacterRepository.Object, mockNotificationService.Object);

			var ex = Assert.ThrowsAsync<InsufficientPermissionException>(async () => await handler.Handle(request, CancellationToken.None));
			Assert.That(ex.Message, Is.EqualTo("You do not have the necessary permissions to perform this operation."));
		}
	}
}
