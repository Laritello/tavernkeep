using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tavernkeep.Application.Interfaces;
using Tavernkeep.Application.UseCases.Characters.Commands.EditSavingThrows;
using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Entities.Pathfinder;
using Tavernkeep.Core.Entities;
using Tavernkeep.Core.Exceptions;
using Tavernkeep.Core.Repositories;
using Tavernkeep.Core.Specifications;
using Tavernkeep.Application.UseCases.Characters.Commands.EditSpeeds;

namespace Tavernkepp.Application.Tests.UseCases.Characters.Commands
{
	public class EditSpeedsCommandTests
	{
		private readonly Guid characterId = Guid.NewGuid();

		private readonly User owner;
		private readonly User master;

		private Character character = default!;

		public EditSpeedsCommandTests()
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
		[TestCase(SpeedType.Walk, false, 25)]
		[TestCase(SpeedType.Burrow, true, 35)]
		[TestCase(SpeedType.Climb, true, 40)]
		[TestCase(SpeedType.Fly, true, 45)]
		[TestCase(SpeedType.Swim, true, 50)]
		public async Task EditSavingThrowsCommand_Success(SpeedType type, bool active, int baseValue)
		{
			var mockCharacterService = new Mock<ICharacterService>();

			mockCharacterService
				.Setup(s => s.RetrieveCharacterForEdit(characterId, owner.Id, It.IsAny<CancellationToken>()))
				.ReturnsAsync(character);

			var request = new EditSpeedsCommand(owner.Id, characterId, new() { { type, new() { Active = active, Base = baseValue } } });
			var handler = new EditSpeedsCommandHandler(mockCharacterService.Object);

			await handler.Handle(request, CancellationToken.None);

			Assert.Multiple(() =>
			{
				Assert.That(character.GetSpeed(type).Active, Is.EqualTo(active));
				Assert.That(character.GetSpeed(type).Base, Is.EqualTo(baseValue));
			});
		}

		[Test]
		public async Task EditSavingThrowsCommand_Success_Master()
		{
			var mockCharacterService = new Mock<ICharacterService>();

			mockCharacterService
				.Setup(s => s.RetrieveCharacterForEdit(characterId, master.Id, It.IsAny<CancellationToken>()))
				.ReturnsAsync(character);

			var request = new EditSpeedsCommand(master.Id, characterId, new() { { SpeedType.Fly, new() { Active = true, Base = 40 } } });
			var handler = new EditSpeedsCommandHandler(mockCharacterService.Object);

			await handler.Handle(request, CancellationToken.None);

			Assert.Multiple(() =>
			{
				Assert.That(character.GetSpeed(SpeedType.Fly).Active, Is.EqualTo(true));
				Assert.That(character.GetSpeed(SpeedType.Fly).Base, Is.EqualTo(40));
			});
		}
	}
}
