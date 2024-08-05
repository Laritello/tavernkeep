using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tavernkeep.Application.UseCases.Users.Queries.GetUser;
using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Entities;
using Tavernkeep.Core.Exceptions;
using Tavernkeep.Core.Repositories;
using Tavernkeep.Core.Specifications.Users;

namespace Tavernkepp.Application.Tests.UseCases.Users.Queries
{
    public class GetUserQueryTests
    {
        private readonly string login = "login";
        private readonly string password = "password";
        private readonly UserRole role = UserRole.Player;

        private User user;

        public GetUserQueryTests() 
        {
            user = new User(login, password, role);
        }

        [Test]
        public async Task GetUserQuery_Success()
        {
            var mockUserRepository = new Mock<IUserRepository>();

            mockUserRepository
                .SetReturnsDefault(Task.FromResult(user));

            var request = new GetUserQuery(user.Id);
            var handler = new GetUserQueryHandler(mockUserRepository.Object);

            var response = await handler.Handle(request, CancellationToken.None);

            Assert.Multiple(() =>
            {
                Assert.That(response, Is.Not.Null);
                Assert.That(response.Id, Is.EqualTo(user.Id));
                Assert.That(response.Login, Is.EqualTo(login));
                Assert.That(response.Role, Is.EqualTo(role));
            });
        }

        [Test]
        public void GetUserQuery_UserNotFound()
        {
            var mockUserRepository = new Mock<IUserRepository>();

            var request = new GetUserQuery(user.Id);
            var handler = new GetUserQueryHandler(mockUserRepository.Object);

            var ex = Assert.ThrowsAsync<BusinessLogicException>(async () => await handler.Handle(request, CancellationToken.None));
            Assert.That(ex.Message, Is.EqualTo("User with specified ID not found."));
        }
    }
}
