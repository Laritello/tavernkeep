using Moq;
using Tavernkeep.Application.UseCases.Lores.Commands.DeleteLore;
using Tavernkeep.Application.UseCases.Lores.Commands.EditLore;
using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Entities;
using Tavernkeep.Core.Exceptions;
using Tavernkeep.Core.Repositories;

namespace Tavernkepp.Application.Tests.UseCases.Lores.Commands
{
    public class EditLoreCommandTests
    {
        private readonly string loreTopic = "Academia";
        private readonly Proficiency loreProficiency = Proficiency.Trained;

        private readonly User owner;
        private readonly User master;

        private Lore lore;
        private Character character;

        public EditLoreCommandTests()
        {
            owner = new User("owner", "owner", UserRole.Player) { Id = Guid.NewGuid() };
            master = new User("master", "master", UserRole.Master) { Id = Guid.NewGuid() };
        }

        [SetUp]
        public void SetUp()
        {
            lore = new Lore() { Topic = loreTopic, Proficiency = Proficiency.Untrained };

            character = new()
            {
                Id = Guid.NewGuid(),
                Owner = owner,
                Lores = [ lore ]
            };
        }

        [Test]
        public async Task EditLoreCommand_Success()
        {
            var mockUserRepository = new Mock<IUserRepository>();
            var mockCharacterRepository = new Mock<ICharacterRepository>();

            mockUserRepository
                .Setup(repo => repo.FindAsync(owner.Id, default!))
                .ReturnsAsync(owner);

            mockCharacterRepository
                .Setup(repo => repo.GetFullCharacterAsync(character.Id, default!))
                .ReturnsAsync(character);

            var request = new EditLoreCommand(owner.Id, character.Id, loreTopic, loreProficiency);
            var handler = new EditLoreCommandHandler(mockUserRepository.Object, mockCharacterRepository.Object);

            var result = await handler.Handle(request, CancellationToken.None);

            Assert.Multiple(() =>
            {
                Assert.That(character.Lores.Any(x => x.Topic == loreTopic), Is.True);
                Assert.That(character.Lores.First(x => x.Topic == loreTopic).Proficiency, Is.EqualTo(loreProficiency));
            });
        }

        [Test]
        public async Task EditLoreCommand_Success_Master()
        {
            var mockUserRepository = new Mock<IUserRepository>();
            var mockCharacterRepository = new Mock<ICharacterRepository>();

            mockUserRepository
                .Setup(repo => repo.FindAsync(master.Id, default!))
                .ReturnsAsync(master);

            mockCharacterRepository
                .Setup(repo => repo.GetFullCharacterAsync(character.Id, default!))
                .ReturnsAsync(character);

            var request = new EditLoreCommand(master.Id, character.Id, loreTopic, loreProficiency);
            var handler = new EditLoreCommandHandler(mockUserRepository.Object, mockCharacterRepository.Object);

            var result = await handler.Handle(request, CancellationToken.None);

            Assert.Multiple(() =>
            {
                Assert.That(character.Lores.Any(x => x.Topic == loreTopic), Is.True);
                Assert.That(character.Lores.First(x => x.Topic == loreTopic).Proficiency, Is.EqualTo(loreProficiency));
            });
        }

        [Test]
        public void EditLoreCommand_InitiatorNotFound()
        {
            var mockUserRepository = new Mock<IUserRepository>();
            var mockCharacterRepository = new Mock<ICharacterRepository>();

            mockCharacterRepository
                .Setup(repo => repo.GetFullCharacterAsync(character.Id, default!))
                .ReturnsAsync(character);

            var request = new EditLoreCommand(owner.Id, character.Id, loreTopic, loreProficiency);
            var handler = new EditLoreCommandHandler(mockUserRepository.Object, mockCharacterRepository.Object);

            var ex = Assert.ThrowsAsync<BusinessLogicException>(async () => await handler.Handle(request, CancellationToken.None));
            Assert.That(ex.Message, Is.EqualTo("User with specified ID doesn't exist."));
        }

        [Test]
        public void EditLoreCommand_CharacterNotFound()
        {
            var mockUserRepository = new Mock<IUserRepository>();
            var mockCharacterRepository = new Mock<ICharacterRepository>();

            mockUserRepository
                .Setup(repo => repo.FindAsync(owner.Id, default!))
                .ReturnsAsync(owner);

            var request = new EditLoreCommand(owner.Id, character.Id, loreTopic, loreProficiency);
            var handler = new EditLoreCommandHandler(mockUserRepository.Object, mockCharacterRepository.Object);

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

            var request = new EditLoreCommand(owner.Id, character.Id, loreTopic, loreProficiency);
            var handler = new EditLoreCommandHandler(mockUserRepository.Object, mockCharacterRepository.Object);

            var ex = Assert.ThrowsAsync<BusinessLogicException>(async () => await handler.Handle(request, CancellationToken.None));
            Assert.That(ex.Message, Is.EqualTo("Character does not have lore skill with this topic."));
        }

        [Test]
        public void EditLoreCommand_NotEnoughPermissions()
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

            var request = new EditLoreCommand(initiatorId, character.Id, loreTopic, loreProficiency);
            var handler = new EditLoreCommandHandler(mockUserRepository.Object, mockCharacterRepository.Object);

            var ex = Assert.ThrowsAsync<InsufficientPermissionException>(async () => await handler.Handle(request, CancellationToken.None));
            Assert.That(ex.Message, Is.EqualTo("You do not have the necessary permissions to perform this operation."));
        }
    }
}
