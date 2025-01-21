using Moq;
using Tavernkeep.Application.Interfaces;
using Tavernkeep.Application.UseCases.Characters.Commands.EditHealth;
using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Entities;
using Tavernkeep.Core.Entities.Pathfinder;
using Tavernkeep.Core.Exceptions;

namespace Tavernkepp.Application.Tests.UseCases.Characters.Commands
{
	public class EditHealthCommandTests
	{
		private readonly Guid characterId = Guid.NewGuid();

		private readonly int currentHealth = 0;
		private readonly int maxHealth = 100;
		private readonly int tempHealth = 15;

		private readonly User owner;
		private readonly User master;

		private Character character = default!;

		public EditHealthCommandTests()
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
		public async Task EditHealthCommand_Success()
		{
			var mockCharacterService = new Mock<ICharacterService>();

			mockCharacterService
				.Setup(s => s.RetrieveCharacterForEdit(characterId, owner.Id, It.IsAny<CancellationToken>()))
				.ReturnsAsync(character);

			var request = new EditHealthCommand(owner.Id, characterId, currentHealth, maxHealth, tempHealth);
			var handler = new EditHealthCommandHandler(mockCharacterService.Object);

			await handler.Handle(request, CancellationToken.None);

			Assert.Multiple(() =>
			{
				Assert.That(character.Health.Current, Is.EqualTo(currentHealth));
				Assert.That(character.Health.Max, Is.EqualTo(maxHealth));
				Assert.That(character.Health.Temporary, Is.EqualTo(tempHealth));
			});
		}

		[Test]
		public async Task EditHealthCommand_Success_Master()
		{
			var mockCharacterService = new Mock<ICharacterService>();

			mockCharacterService
				.Setup(s => s.RetrieveCharacterForEdit(characterId, master.Id, It.IsAny<CancellationToken>()))
				.ReturnsAsync(character);

			var request = new EditHealthCommand(master.Id, characterId, currentHealth, maxHealth, tempHealth);
			var handler = new EditHealthCommandHandler(mockCharacterService.Object);

			await handler.Handle(request, CancellationToken.None);

			Assert.Multiple(() =>
			{
				Assert.That(character.Health.Current, Is.EqualTo(currentHealth));
				Assert.That(character.Health.Max, Is.EqualTo(maxHealth));
				Assert.That(character.Health.Temporary, Is.EqualTo(tempHealth));
			});
		}

		[Test]
		public void EditHealthCommand_NegativeMaxHealth()
		{
			var mockCharacterService = new Mock<ICharacterService>();

			mockCharacterService
				.Setup(s => s.RetrieveCharacterForEdit(characterId, owner.Id, It.IsAny<CancellationToken>()))
				.ReturnsAsync(character);

			var request = new EditHealthCommand(owner.Id, characterId, currentHealth, -1, tempHealth);
			var handler = new EditHealthCommandHandler(mockCharacterService.Object);

			Assert.ThatAsync(async () => await handler.Handle(request, CancellationToken.None),
				Throws.TypeOf<BusinessLogicException>()
				.With.Message.EqualTo($"{nameof(request.Max)} can't be below zero."));
		}

		[Test]
		public void EditHealthCommand_NegativeCurrentHealth()
		{
			var mockCharacterService = new Mock<ICharacterService>();

			mockCharacterService
				.Setup(s => s.RetrieveCharacterForEdit(characterId, owner.Id, It.IsAny<CancellationToken>()))
				.ReturnsAsync(character);

			var request = new EditHealthCommand(owner.Id, characterId, -1, maxHealth, tempHealth);
			var handler = new EditHealthCommandHandler(mockCharacterService.Object);

			Assert.ThatAsync(async () => await handler.Handle(request, CancellationToken.None),
				Throws.TypeOf<BusinessLogicException>()
				.With.Message.EqualTo($"{nameof(request.Current)} can't be below zero."));
		}

		[Test]
		public void EditHealthCommand_NegativeTemporaryHealth()
		{
			var mockCharacterService = new Mock<ICharacterService>();

			mockCharacterService
				.Setup(s => s.RetrieveCharacterForEdit(characterId, owner.Id, It.IsAny<CancellationToken>()))
				.ReturnsAsync(character);

			var request = new EditHealthCommand(owner.Id, characterId, currentHealth, maxHealth, -1);
			var handler = new EditHealthCommandHandler(mockCharacterService.Object);

			Assert.ThatAsync(async () => await handler.Handle(request, CancellationToken.None),
				Throws.TypeOf<BusinessLogicException>()
				.With.Message.EqualTo($"{nameof(request.Temporary)} can't be below zero."));
		}
	}
}
