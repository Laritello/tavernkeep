using Moq;
using Tavernkeep.Application.Actions.Characters.Commands.DeleteCharacter;
using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Entities;
using Tavernkeep.Core.Exceptions;
using Tavernkeep.Core.Repositories;

namespace Tavernkepp.Application.Tests.UseCases.Characters.Commands
{
    public class DeleteCharacterCommandTests
    {
        private readonly Guid characterId = Guid.NewGuid();

        private readonly User owner;
        private readonly User master;

        private Character character;

        public DeleteCharacterCommandTests()
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
        public async Task DeleteCharacterCommand_Success()
        {
            var mockUserRepository = new Mock<IUserRepository>();
            var mockCharacterRepository = new Mock<ICharacterRepository>();

            mockUserRepository
                .Setup(repo => repo.FindAsync(owner.Id, default!))
                .ReturnsAsync(owner);

            mockCharacterRepository
                .Setup(repo => repo.FindAsync(characterId, default!))
                .ReturnsAsync(character);

            var request = new DeleteCharacterCommand(owner.Id, characterId);
            var handler = new DeleteCharacterCommandHandler(mockUserRepository.Object, mockCharacterRepository.Object);

            await handler.Handle(request, CancellationToken.None);
        }

        [Test]
        public async Task DeleteCharacterCommand_Success_Master()
        {
            var mockUserRepository = new Mock<IUserRepository>();
            var mockCharacterRepository = new Mock<ICharacterRepository>();

            mockUserRepository
                .Setup(repo => repo.FindAsync(master.Id, default!))
                .ReturnsAsync(master);

            mockCharacterRepository
                .Setup(repo => repo.FindAsync(characterId, default!))
                .ReturnsAsync(character);

            var request = new DeleteCharacterCommand(master.Id, characterId);
            var handler = new DeleteCharacterCommandHandler(mockUserRepository.Object, mockCharacterRepository.Object);

            await handler.Handle(request, CancellationToken.None);
        }

        [Test]
        public void DeleteCharacterCommand_InitiatorNotFound()
        {
            var mockUserRepository = new Mock<IUserRepository>();
            var mockCharacterRepository = new Mock<ICharacterRepository>();

            mockCharacterRepository
                .Setup(repo => repo.FindAsync(characterId, default!))
                .ReturnsAsync(character);

            var request = new DeleteCharacterCommand(owner.Id, characterId);
            var handler = new DeleteCharacterCommandHandler(mockUserRepository.Object, mockCharacterRepository.Object);

            var ex = Assert.ThrowsAsync<BusinessLogicException>(async () => await handler.Handle(request, CancellationToken.None));
            Assert.That(ex.Message, Is.EqualTo("User with specified ID doesn't exist."));
        }

        [Test]
        public void DeleteCharacterCommand_CharacterNotFound()
        {
            var mockUserRepository = new Mock<IUserRepository>();
            var mockCharacterRepository = new Mock<ICharacterRepository>();

            mockUserRepository
                .Setup(repo => repo.FindAsync(owner.Id, default!))
                .ReturnsAsync(owner);

            var request = new DeleteCharacterCommand(owner.Id, characterId);
            var handler = new DeleteCharacterCommandHandler(mockUserRepository.Object, mockCharacterRepository.Object);

            var ex = Assert.ThrowsAsync<BusinessLogicException>(async () => await handler.Handle(request, CancellationToken.None));
            Assert.That(ex.Message, Is.EqualTo("Character not found"));
        }

        [Test]
        public void DeleteCharacterCommand_NotEnoughPermissions()
        {
            var mockUserRepository = new Mock<IUserRepository>();
            var mockCharacterRepository = new Mock<ICharacterRepository>();
            var initiatorId = Guid.NewGuid();

            mockUserRepository
                .Setup(repo => repo.FindAsync(initiatorId, default!))
                .ReturnsAsync(new User(string.Empty, string.Empty, UserRole.Player));

            mockCharacterRepository
                .Setup(repo => repo.FindAsync(characterId, default!))
                .ReturnsAsync(character);

            var request = new DeleteCharacterCommand(initiatorId, characterId);
            var handler = new DeleteCharacterCommandHandler(mockUserRepository.Object, mockCharacterRepository.Object);

            var ex = Assert.ThrowsAsync<InsufficientPermissionException>(async () => await handler.Handle(request, CancellationToken.None));
            Assert.That(ex.Message, Is.EqualTo("You do not have the necessary permissions to perform this operation."));
        }
    }
}
