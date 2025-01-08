using Moq;
using Tavernkeep.Application.Interfaces;
using Tavernkeep.Application.UseCases.Characters.Commands.EditArmor;
using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Entities;
using Tavernkeep.Core.Entities.Pathfinder;
using Tavernkeep.Core.Exceptions;
using Tavernkeep.Core.Repositories;
using Tavernkeep.Core.Specifications;

namespace Tavernkepp.Application.Tests.UseCases.Characters.Commands
{
	internal class EditArmorCommandTests
	{
		private readonly Guid characterId = Guid.NewGuid();

		private readonly User owner;
		private readonly User master;

		private Character character = default!;

		public EditArmorCommandTests()
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

			character.Armor.Equipped.Type = ArmorType.Medium;
			character.Armor.Equipped.Bonus = 2;
			character.Armor.Equipped.HasDexterityCap = true;
			character.Armor.Equipped.DexterityCap = 3;

			character.Armor.Proficiencies[ArmorType.Unarmored] = Proficiency.Untrained;
			character.Armor.Proficiencies[ArmorType.Light] = Proficiency.Trained;
			character.Armor.Proficiencies[ArmorType.Medium] = Proficiency.Expert;
			character.Armor.Proficiencies[ArmorType.Heavy] = Proficiency.Master;
		}

		[Test]
		public async Task EditArmorCommand_Success()
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

			Dictionary<ArmorType, Proficiency> proficiencies = new()
			{
				{ ArmorType.Unarmored, Proficiency.Master },
				{ ArmorType.Light, Proficiency.Expert },
				{ ArmorType.Medium, Proficiency.Trained },
				{ ArmorType.Heavy, Proficiency.Untrained },
			};

			var request = new EditArmorCommand(owner.Id, characterId, ArmorType.Unarmored, 1, false, 0, proficiencies);
			var handler = new EditArmorCommandHandler(mockUserRepository.Object, mockCharacterRepository.Object, mockNotificationService.Object);

			await handler.Handle(request, CancellationToken.None);

			Assert.Multiple(() =>
			{
				Assert.That(character.Armor.Equipped.Type, Is.EqualTo(ArmorType.Unarmored));
				Assert.That(character.Armor.Equipped.Bonus, Is.EqualTo(1));
				Assert.That(character.Armor.Equipped.HasDexterityCap, Is.EqualTo(false));
				Assert.That(character.Armor.Equipped.DexterityCap, Is.EqualTo(0));

				Assert.That(character.Armor.Proficiencies[ArmorType.Unarmored], Is.EqualTo(Proficiency.Master));
				Assert.That(character.Armor.Proficiencies[ArmorType.Light], Is.EqualTo(Proficiency.Expert));
				Assert.That(character.Armor.Proficiencies[ArmorType.Medium], Is.EqualTo(Proficiency.Trained));
				Assert.That(character.Armor.Proficiencies[ArmorType.Heavy], Is.EqualTo(Proficiency.Untrained));
			});
		}

		[Test]
		[TestCase(ArmorType.Unarmored)]
		[TestCase(ArmorType.Light)]
		[TestCase(ArmorType.Medium)]
		[TestCase(ArmorType.Heavy)]
		public async Task EditArmorCommand_Success_Master(ArmorType type)
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

			Dictionary<ArmorType, Proficiency> proficiencies = new()
			{
				{ ArmorType.Unarmored, Proficiency.Master },
				{ ArmorType.Light, Proficiency.Expert },
				{ ArmorType.Medium, Proficiency.Trained },
				{ ArmorType.Heavy, Proficiency.Untrained },
			};

			var request = new EditArmorCommand(master.Id, characterId, ArmorType.Unarmored, 1, false, 0, proficiencies);
			var handler = new EditArmorCommandHandler(mockUserRepository.Object, mockCharacterRepository.Object, mockNotificationService.Object);

			await handler.Handle(request, CancellationToken.None);

			Assert.Multiple(() =>
			{
				Assert.That(character.Armor.Equipped.Type, Is.EqualTo(ArmorType.Unarmored));
				Assert.That(character.Armor.Equipped.Bonus, Is.EqualTo(1));
				Assert.That(character.Armor.Equipped.HasDexterityCap, Is.EqualTo(false));
				Assert.That(character.Armor.Equipped.DexterityCap, Is.EqualTo(0));

				Assert.That(character.Armor.Proficiencies[ArmorType.Unarmored], Is.EqualTo(Proficiency.Master));
				Assert.That(character.Armor.Proficiencies[ArmorType.Light], Is.EqualTo(Proficiency.Expert));
				Assert.That(character.Armor.Proficiencies[ArmorType.Medium], Is.EqualTo(Proficiency.Trained));
				Assert.That(character.Armor.Proficiencies[ArmorType.Heavy], Is.EqualTo(Proficiency.Untrained));
			});
		}

		[Test]
		public void EditArmorProficiencyCommand_InitiatorNotFound()
		{
			var mockUserRepository = new Mock<IUserRepository>();
			var mockCharacterRepository = new Mock<ICharacterRepository>();
			var mockNotificationService = new Mock<INotificationService>();

			mockCharacterRepository
				.Setup(repo => repo.GetFullCharacterAsync(characterId, It.IsAny<CancellationToken>()))
				.ReturnsAsync(character);

			var request = new EditArmorCommand(owner.Id, characterId, ArmorType.Unarmored, 1, false, 0, []);
			var handler = new EditArmorCommandHandler(mockUserRepository.Object, mockCharacterRepository.Object, mockNotificationService.Object);

			Assert.ThatAsync(async () => await handler.Handle(request, CancellationToken.None),
				Throws.TypeOf<BusinessLogicException>()
				.With.Message.EqualTo("User with specified ID doesn't exist."));
		}

		[Test]
		public void EditArmorProficiencyCommand_CharacterNotFound()
		{
			var mockUserRepository = new Mock<IUserRepository>();
			var mockCharacterRepository = new Mock<ICharacterRepository>();
			var mockNotificationService = new Mock<INotificationService>();

			mockUserRepository
				.Setup(repo => repo.FindAsync(owner.Id, It.IsAny<ISpecification<User>>(), It.IsAny<CancellationToken>()))
				.ReturnsAsync(owner);

			var request = new EditArmorCommand(owner.Id, characterId, ArmorType.Unarmored, 1, false, 0, []);
			var handler = new EditArmorCommandHandler(mockUserRepository.Object, mockCharacterRepository.Object, mockNotificationService.Object);

			Assert.ThatAsync(async () => await handler.Handle(request, CancellationToken.None),
				Throws.TypeOf<BusinessLogicException>()
				.With.Message.EqualTo("Character with specified ID doesn't exist."));
		}

		[Test]
		public void EditArmorProficiencyCommand_NotEnoughPermissions()
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

			var request = new EditArmorCommand(initiatorId, characterId, ArmorType.Unarmored, 1, false, 0, []);
			var handler = new EditArmorCommandHandler(mockUserRepository.Object, mockCharacterRepository.Object, mockNotificationService.Object);

			Assert.ThatAsync(async () => await handler.Handle(request, CancellationToken.None),
				Throws.TypeOf<InsufficientPermissionException>()
				.With.Message.EqualTo("You do not have the necessary permissions to perform this operation."));
		}
	}
}
