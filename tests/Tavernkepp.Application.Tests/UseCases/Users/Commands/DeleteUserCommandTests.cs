using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tavernkeep.Application.Actions.Characters.Commands.DeleteCharacter;
using Tavernkeep.Application.Actions.Users.Commands.DeleteUser;
using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Entities;
using Tavernkeep.Core.Exceptions;
using Tavernkeep.Core.Repositories;

namespace Tavernkepp.Application.Tests.UseCases.Users.Commands
{
    public class DeleteUserCommandTests
    {
        private readonly User user;
        private readonly User master;

        public DeleteUserCommandTests()
        {
            user = new User("user", "user", UserRole.Player) { Id = Guid.NewGuid() };
            master = new User("master", "master", UserRole.Master) { Id = Guid.NewGuid() };
        }

        [Test]
        public async Task DeleteCharacterCommand_Success()
        {
            var mockUserRepository = new Mock<IUserRepository>();

            mockUserRepository
                .Setup(repo => repo.FindAsync(user.Id, default!))
                .ReturnsAsync(user);

            var request = new DeleteUserCommand(user.Id);
            var handler = new DeleteUserCommandHandler(mockUserRepository.Object);

            await handler.Handle(request, CancellationToken.None);
        }

        [Test]
        public void DeleteCharacterCommand_UserNotFound()
        {
            var mockUserRepository = new Mock<IUserRepository>();
            var mockCharacterRepository = new Mock<ICharacterRepository>();

            var request = new DeleteUserCommand(user.Id);
            var handler = new DeleteUserCommandHandler(mockUserRepository.Object);

            var ex = Assert.ThrowsAsync<BusinessLogicException>(async () => await handler.Handle(request, CancellationToken.None));
            Assert.That(ex.Message, Is.EqualTo("User with specified ID doesn't exist."));
        }
    }
}
