using Moq;
using Tavernkeep.Application.Interfaces;
using Tavernkeep.Application.UseCases.Characters.Commands.CreateCharacter;
using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Entities;
using Tavernkeep.Core.Entities.Pathfinder;
using Tavernkeep.Core.Exceptions;
using Tavernkeep.Core.Repositories;
using Tavernkeep.Core.Specifications;

namespace Tavernkepp.Application.Tests.UseCases.Characters.Commands
{
	public class CreateCharacterCommandTests
	{
		private readonly Guid userId = Guid.NewGuid();

		private readonly string name = "default_character";
		private readonly User owner;
		private readonly Character character;

		public CreateCharacterCommandTests()
		{
			owner = new(string.Empty, string.Empty, UserRole.Player) { Id = userId };
			character = new() { Id = Guid.NewGuid(), Name = name, Owner = owner };
		}

		[Test]
		public async Task CreateCharacterCommand_Success()
		{
			var mockUserRepository = new Mock<IUserRepository>();
			var mockCharacterService = new Mock<ICharacterService>();
			var mockNotificationService = new Mock<INotificationService>();

			mockUserRepository
				.Setup(repo => repo.FindAsync(userId, It.IsAny<ISpecification<User>>(), It.IsAny<CancellationToken>()))
				.ReturnsAsync(owner);

			mockCharacterService
				.Setup(service => service.CreateCharacterAsync(owner, name, It.IsAny<CancellationToken>()))
				.ReturnsAsync(character);

			var request = new CreateCharacterCommand(userId, name);
			var handler = new CreateCharacterCommandHandler(mockUserRepository.Object, mockCharacterService.Object, mockNotificationService.Object);

			var response = await handler.Handle(request, CancellationToken.None);

			Assert.Multiple(() =>
			{
				Assert.That(response.Name, Is.EqualTo(name));
				Assert.That(response.Owner.Id, Is.EqualTo(userId));
			});
		}

		[Test]
		public void CreateCharacterCommand_OwnerNotFound()
		{
			var mockUserRepository = new Mock<IUserRepository>();
			var mockCharacterService = new Mock<ICharacterService>();
			var mockNotificationService = new Mock<INotificationService>();

			var request = new CreateCharacterCommand(userId, name);
			var handler = new CreateCharacterCommandHandler(mockUserRepository.Object, mockCharacterService.Object, mockNotificationService.Object);

			var ex = Assert.ThrowsAsync<BusinessLogicException>(async () => await handler.Handle(request, CancellationToken.None));
			Assert.That(ex.Message, Is.EqualTo("Owner with specified ID doesn't exist."));
		}
	}
}
