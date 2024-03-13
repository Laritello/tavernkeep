using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tavernkeep.Application.Actions.Characters.Commands.DeleteCharacter;
using Tavernkeep.Application.UseCases.Lores.Commands.DeleteLore;
using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Entities;
using Tavernkeep.Core.Exceptions;
using Tavernkeep.Core.Repositories;

namespace Tavernkepp.Application.Tests.UseCases.Lores.Commands
{
    public class DeleteLoreCommandTests
    {
        private readonly string loreTopic = "Academia";

        private readonly User owner;
        private readonly User master;
        private readonly Lore lore;

        private Character character;

        public DeleteLoreCommandTests()
        {
            owner = new User("owner", "owner", UserRole.Player) { Id = Guid.NewGuid() };
            master = new User("master", "master", UserRole.Master) { Id = Guid.NewGuid() };
            lore = new Lore()
            {
                Proficiency = Proficiency.Trained,
                Topic = loreTopic,
            };
        }

        [SetUp]
        public void SetUp()
        {
            character = new()
            {
                Id = Guid.NewGuid(),
                Owner = owner,
                Lores = [ lore ],
            };
        }

        [Test]
        public async Task DeleteLoreCommand_Success()
        {
            var mockUserRepository = new Mock<IUserRepository>();
            var mockCharacterRepository = new Mock<ICharacterRepository>();

            mockUserRepository
                .Setup(repo => repo.FindAsync(owner.Id, default!))
                .ReturnsAsync(owner);

            mockCharacterRepository
                .Setup(repo => repo.GetFullCharacterAsync(character.Id, default!))
                .ReturnsAsync(character);

            var request = new DeleteLoreCommand(owner.Id, character.Id, loreTopic);
            var handler = new DeleteLoreCommandHandler(mockUserRepository.Object, mockCharacterRepository.Object);

            await handler.Handle(request, CancellationToken.None);
        }

        [Test]
        public async Task DeleteLoreCommand_Success_Master()
        {
            var mockUserRepository = new Mock<IUserRepository>();
            var mockCharacterRepository = new Mock<ICharacterRepository>();

            mockUserRepository
                .Setup(repo => repo.FindAsync(master.Id, default!))
                .ReturnsAsync(master);

            mockCharacterRepository
                .Setup(repo => repo.GetFullCharacterAsync(character.Id, default!))
                .ReturnsAsync(character);

            var request = new DeleteLoreCommand(master.Id, character.Id, loreTopic);
            var handler = new DeleteLoreCommandHandler(mockUserRepository.Object, mockCharacterRepository.Object);

            await handler.Handle(request, CancellationToken.None);
        }

        [Test]
        public void DeleteLoreCommand_InitiatorNotFound()
        {
            var mockUserRepository = new Mock<IUserRepository>();
            var mockCharacterRepository = new Mock<ICharacterRepository>();

            mockCharacterRepository
                .Setup(repo => repo.GetFullCharacterAsync(character.Id, default!))
                .ReturnsAsync(character);

            var request = new DeleteLoreCommand(owner.Id, character.Id, loreTopic);
            var handler = new DeleteLoreCommandHandler(mockUserRepository.Object, mockCharacterRepository.Object);

            var ex = Assert.ThrowsAsync<BusinessLogicException>(async () => await handler.Handle(request, CancellationToken.None));
            Assert.That(ex.Message, Is.EqualTo("User with specified ID doesn't exist."));
        }

        [Test]
        public void DeleteLoreCommand_CharacterNotFound()
        {
            var mockUserRepository = new Mock<IUserRepository>();
            var mockCharacterRepository = new Mock<ICharacterRepository>();

            mockUserRepository
                .Setup(repo => repo.FindAsync(owner.Id, default!))
                .ReturnsAsync(owner);

            var request = new DeleteLoreCommand(owner.Id, character.Id, loreTopic);
            var handler = new DeleteLoreCommandHandler(mockUserRepository.Object, mockCharacterRepository.Object);

            var ex = Assert.ThrowsAsync<BusinessLogicException>(async () => await handler.Handle(request, CancellationToken.None));
            Assert.That(ex.Message, Is.EqualTo("Character with specified ID doesn't exist."));
        }

        [Test]
        public void DeleteLoreCommand_LoreNotFound()
        {
            var mockUserRepository = new Mock<IUserRepository>();
            var mockCharacterRepository = new Mock<ICharacterRepository>();

            character.Lores.Clear();

            mockUserRepository
                .Setup(repo => repo.FindAsync(owner.Id, default!))
                .ReturnsAsync(owner);

            mockCharacterRepository
                .Setup(repo => repo.GetFullCharacterAsync(character.Id, default!))
                .ReturnsAsync(character);

            var request = new DeleteLoreCommand(owner.Id, character.Id, loreTopic);
            var handler = new DeleteLoreCommandHandler(mockUserRepository.Object, mockCharacterRepository.Object);

            var ex = Assert.ThrowsAsync<BusinessLogicException>(async () => await handler.Handle(request, CancellationToken.None));
            Assert.That(ex.Message, Is.EqualTo("Character does not have lore skill with this topic."));
        }

        [Test]
        public void DeleteLoreCommand_NotEnoughPermissions()
        {
            var mockUserRepository = new Mock<IUserRepository>();
            var mockCharacterRepository = new Mock<ICharacterRepository>();
            var initiatorId = Guid.NewGuid();

            mockUserRepository
                .Setup(repo => repo.FindAsync(initiatorId, default!))
                .ReturnsAsync(new User(string.Empty, string.Empty, UserRole.Player));

            mockCharacterRepository
                .Setup(repo => repo.GetFullCharacterAsync(character.Id, default!))
                .ReturnsAsync(character);

            var request = new DeleteLoreCommand(initiatorId, character.Id, loreTopic);
            var handler = new DeleteLoreCommandHandler(mockUserRepository.Object, mockCharacterRepository.Object);

            var ex = Assert.ThrowsAsync<InsufficientPermissionException>(async () => await handler.Handle(request, CancellationToken.None));
            Assert.That(ex.Message, Is.EqualTo("You do not have the necessary permissions to perform this operation."));
        }
    }
}
