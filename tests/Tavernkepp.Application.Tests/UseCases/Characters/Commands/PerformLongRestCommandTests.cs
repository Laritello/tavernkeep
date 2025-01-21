using Moq;
using Tavernkeep.Application.Interfaces;
using Tavernkeep.Application.UseCases.Characters.Commands.PerformLongRest;
using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Entities;
using Tavernkeep.Core.Entities.Pathfinder;
using Tavernkeep.Core.Entities.Pathfinder.Conditions;
using Tavernkeep.Core.Repositories;

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
			character = CharacterGenerator.Generate(characterId, owner);
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
			var mockCharacterService = new Mock<ICharacterService>();
			var mockConditionRepository = new Mock<IConditionMetadataRepository>();

			mockCharacterService
				.Setup(s => s.RetrieveCharacterForEdit(characterId, owner.Id, It.IsAny<CancellationToken>()))
				.ReturnsAsync(character);

			character.Level = level;
			character.Abilities["Constitution"].Score = constitutionScore;

			character.Health.Temporary = 0;
			character.Health.Max = 100;
			character.Health.Current = currentHealth;

			var calculatedNewHealth = currentHealth + level * ((constitutionScore - 10) / 2);

			var request = new PerformLongRestCommand(owner.Id, characterId, false, false);
			var handler = new PerformLongRestCommandHandler(mockCharacterService.Object, mockConditionRepository.Object);

			await handler.Handle(request, CancellationToken.None);

			Assert.Multiple(() =>
			{
				Assert.That(character.Health.Current, Is.EqualTo(Math.Min(character.Health.Max, calculatedNewHealth)));
			});
		}

		[Test]
		public async Task PerformLongRestCommand_Success_NoComfort()
		{
			var mockCharacterService = new Mock<ICharacterService>();
			var mockConditionsRepository = new Mock<IConditionMetadataRepository>();

			mockCharacterService
				.Setup(s => s.RetrieveCharacterForEdit(characterId, owner.Id, It.IsAny<CancellationToken>()))
				.ReturnsAsync(character);

			character.Level = 6;
			character.Abilities["Constitution"].Score = 14;

			character.Health.Temporary = 0;
			character.Health.Max = 100;
			character.Health.Current = 18;

			var request = new PerformLongRestCommand(owner.Id, characterId, true, false);
			var handler = new PerformLongRestCommandHandler(mockCharacterService.Object, mockConditionsRepository.Object);

			await handler.Handle(request, CancellationToken.None);

			Assert.Multiple(() =>
			{
				Assert.That(character.Health.Current, Is.EqualTo(24));
			});
		}

		[Test]
		public async Task PerformLongRestCommand_Success_InArmor()
		{
			var mockCharacterService = new Mock<ICharacterService>();
			var mockConditionsRepository = new Mock<IConditionMetadataRepository>();

			mockCharacterService
				.Setup(s => s.RetrieveCharacterForEdit(characterId, owner.Id, It.IsAny<CancellationToken>()))
				.ReturnsAsync(character);

			mockConditionsRepository
				.Setup(repo => repo.GetConditionAsync("Fatigued", It.IsAny<CancellationToken>()))
				.ReturnsAsync(new ConditionTemplate() { Name = "Fatigued" });

			character.Level = 6;
			character.Abilities["Constitution"].Score = 14;

			character.Health.Temporary = 0;
			character.Health.Max = 100;
			character.Health.Current = 18;

			var request = new PerformLongRestCommand(owner.Id, characterId, false, true);
			var handler = new PerformLongRestCommandHandler(mockCharacterService.Object, mockConditionsRepository.Object);

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
			var mockCharacterService = new Mock<ICharacterService>();
			var mockConditionsRepository = new Mock<IConditionMetadataRepository>();

			mockCharacterService
				.Setup(s => s.RetrieveCharacterForEdit(characterId, master.Id, It.IsAny<CancellationToken>()))
				.ReturnsAsync(character);

			character.Level = 6;
			character.Abilities["Constitution"].Score = 14;

			character.Health.Temporary = 0;
			character.Health.Max = 100;
			character.Health.Current = 18;

			var request = new PerformLongRestCommand(master.Id, characterId, false, false);
			var handler = new PerformLongRestCommandHandler(mockCharacterService.Object, mockConditionsRepository.Object);

			await handler.Handle(request, CancellationToken.None);

			Assert.That(character.Health.Current, Is.EqualTo(30));
		}
	}
}
