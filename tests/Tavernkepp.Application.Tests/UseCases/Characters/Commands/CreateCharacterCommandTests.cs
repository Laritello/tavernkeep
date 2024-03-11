using Moq;
using Tavernkeep.Application.Actions.Characters.Commands.CreateCharacter;
using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Entities;
using Tavernkeep.Core.Exceptions;
using Tavernkeep.Core.Repositories;

namespace Tavernkepp.Application.Tests.UseCases.Characters.Commands
{
    public class CreateCharacterCommandTests
    {
        private readonly Guid userId = Guid.NewGuid();

        private readonly string name = "default_character";

        private readonly User owner;

        public CreateCharacterCommandTests()
        {
            owner = new(string.Empty, string.Empty, UserRole.Player) { Id = userId };
        }

        [Test]
        public async Task CreateCharacterCommand_Success()
        {
            var mockUserRepository = new Mock<IUserRepository>();
            var mockCharacterRepository = new Mock<ICharacterRepository>();

            mockUserRepository
                .Setup(repo => repo.FindAsync(userId, default!))
                .ReturnsAsync(owner);

            var request = new CreateCharacterCommand(userId, name);
            var handler = new CreateCharacterCommandHandler(mockUserRepository.Object, mockCharacterRepository.Object);

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
            var mockCharacterRepository = new Mock<ICharacterRepository>();

            var request = new CreateCharacterCommand(userId, name);
            var handler = new CreateCharacterCommandHandler(mockUserRepository.Object, mockCharacterRepository.Object);

            var ex = Assert.ThrowsAsync<BusinessLogicException>(async () => await handler.Handle(request, CancellationToken.None));
            Assert.That(ex.Message, Is.EqualTo("Owner with specified ID doesn't exist."));
        }
    }
}
