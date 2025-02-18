using Moq;
using Tavernkeep.Application.Interfaces;
using Tavernkeep.Application.UseCases.Characters.Commands.EditConditions;
using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Entities;
using Tavernkeep.Core.Entities.Pathfinder;
using Tavernkeep.Core.Entities.Pathfinder.Conditions;
using Tavernkeep.Core.Repositories;

namespace Tavernkepp.Application.Tests.UseCases.Characters.Commands
{
	internal class EditConditionsCommandTests
	{
		private readonly Guid characterId = Guid.NewGuid();

		private readonly User owner;
		private readonly User master;

		private readonly List<ConditionInformation> conditions =
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
						Targets = ["Perception"],
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
			character = CharacterGenerator.Generate(characterId, owner);

			character.Abilities["Wisdom"].Score = 16;
			character.Skills["Perception"].Proficiency = Proficiency.Trained;
		}

		[Test]
		public async Task EditConditionsCommand_Success()
		{
			var mockCharacterService = new Mock<ICharacterService>();
			var mockConditionLibraryRepository = new Mock<IConditionLibraryRepository>();

			mockCharacterService
				.Setup(s => s.RetrieveCharacterForAction(characterId, owner.Id, It.IsAny<CancellationToken>()))
				.ReturnsAsync(character);
			mockConditionLibraryRepository
				.Setup(repo => repo.GetConditionAsync("Blinded", It.IsAny<CancellationToken>()))
				.ReturnsAsync(conditions[0]);

			int basePerception = character.Skills["Perception"].Bonus;

			var request = new EditConditionsCommand(owner.Id, characterId, [new() { Name = "Blinded" }]);
			var handler = new EditConditionsCommandHandler(mockCharacterService.Object, mockConditionLibraryRepository.Object);

			await handler.Handle(request, CancellationToken.None);

			Assert.Multiple(() =>
			{
				Assert.That(character.Conditions, Has.Count.EqualTo(1));
				Assert.That(character.Conditions.FirstOrDefault(x => x.Name == "Blinded"), Is.Not.Null);
				Assert.That(character.Skills["Perception"].Bonus, Is.EqualTo(basePerception - 4));
			});
		}

		[Test]
		public async Task EditConditionsCommand_Success_Master()
		{
			var mockCharacterService = new Mock<ICharacterService>();
			var mockConditionLibraryRepository = new Mock<IConditionLibraryRepository>();

			mockCharacterService
				.Setup(s => s.RetrieveCharacterForAction(characterId, master.Id, It.IsAny<CancellationToken>()))
				.ReturnsAsync(character);
			mockConditionLibraryRepository
				.Setup(repo => repo.GetConditionAsync("Blinded", It.IsAny<CancellationToken>()))
				.ReturnsAsync(conditions[0]);

			int basePerception = character.Skills["Perception"].Bonus;

			var request = new EditConditionsCommand(master.Id, characterId, [new() { Name = "Blinded" }]);
			var handler = new EditConditionsCommandHandler(mockCharacterService.Object, mockConditionLibraryRepository.Object);

			await handler.Handle(request, CancellationToken.None);

			Assert.Multiple(() =>
			{
				Assert.That(character.Conditions, Has.Count.EqualTo(1));
				Assert.That(character.Conditions.FirstOrDefault(x => x.Name == "Blinded"), Is.Not.Null);
				Assert.That(character.Skills["Perception"].Bonus, Is.EqualTo(basePerception - 4));
			});
		}
	}
}
