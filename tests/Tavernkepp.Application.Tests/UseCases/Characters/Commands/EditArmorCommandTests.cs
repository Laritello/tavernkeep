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
			var mockCharacterService = new Mock<ICharacterService>();

			mockCharacterService
				.Setup(s => s.RetrieveCharacterForEdit(characterId, owner.Id, It.IsAny<CancellationToken>()))
				.ReturnsAsync(character);

			Dictionary<ArmorType, Proficiency> proficiencies = new()
			{
				{ ArmorType.Unarmored, Proficiency.Master },
				{ ArmorType.Light, Proficiency.Expert },
				{ ArmorType.Medium, Proficiency.Trained },
				{ ArmorType.Heavy, Proficiency.Untrained },
			};

			var request = new EditArmorCommand(owner.Id, characterId, ArmorType.Unarmored, 1, false, 0, proficiencies);
			var handler = new EditArmorCommandHandler(mockCharacterService.Object);

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
			var mockCharacterService = new Mock<ICharacterService>();

			mockCharacterService
				.Setup(s => s.RetrieveCharacterForEdit(characterId, master.Id, It.IsAny<CancellationToken>()))
				.ReturnsAsync(character);

			Dictionary<ArmorType, Proficiency> proficiencies = new()
			{
				{ ArmorType.Unarmored, Proficiency.Master },
				{ ArmorType.Light, Proficiency.Expert },
				{ ArmorType.Medium, Proficiency.Trained },
				{ ArmorType.Heavy, Proficiency.Untrained },
			};

			var request = new EditArmorCommand(master.Id, characterId, ArmorType.Unarmored, 1, false, 0, proficiencies);
			var handler = new EditArmorCommandHandler(mockCharacterService.Object);

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
	}
}
