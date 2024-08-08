using Moq;
using Tavernkeep.Application.UseCases.Users.Commands.EditUser;
using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Entities;
using Tavernkeep.Core.Exceptions;
using Tavernkeep.Core.Repositories;

namespace Tavernkepp.Application.Tests.UseCases.Users.Commands
{
	public class EditUserCommandTests
	{
		private readonly string login = "new_login";
		private readonly string password = "new_password";
		private readonly UserRole role = UserRole.Moderator;

		private User user;

		[SetUp]
		public void SetUp()
		{
			user = new(string.Empty, string.Empty, UserRole.Player)
			{
				Id = Guid.NewGuid(),
			};
		}

		[Test]
		public async Task EditUserCommand_Success()
		{
			var mockUserRepository = new Mock<IUserRepository>();

			mockUserRepository
				.Setup(repo => repo.FindAsync(user.Id, default!))
				.ReturnsAsync(user);

			var request = new EditUserCommand(user.Id, login, password, role);
			var handler = new EditUserCommandHandler(mockUserRepository.Object);

			var response = await handler.Handle(request, CancellationToken.None);

			Assert.Multiple(() =>
			{
				Assert.That(response.Login, Is.EqualTo(login));
				Assert.That(response.Password, Is.EqualTo(password));
				Assert.That(response.Role, Is.EqualTo(role));
			});
		}

		[Test]
		public void EditUserCommand_UserNotFound()
		{
			var mockUserRepository = new Mock<IUserRepository>();

			var request = new EditUserCommand(user.Id, login, password, role);
			var handler = new EditUserCommandHandler(mockUserRepository.Object);

			var ex = Assert.ThrowsAsync<BusinessLogicException>(async () => await handler.Handle(request, CancellationToken.None));
			Assert.That(ex.Message, Is.EqualTo("User with specified ID doesn't exist."));
		}

		[Test]
		public void EditUserCommand_EmptyLogin()
		{
			var mockUserRepository = new Mock<IUserRepository>();

			mockUserRepository
				.Setup(repo => repo.FindAsync(user.Id, default!))
				.ReturnsAsync(user);

			var request = new EditUserCommand(user.Id, string.Empty, password, role);
			var handler = new EditUserCommandHandler(mockUserRepository.Object);

			var ex = Assert.ThrowsAsync<BusinessLogicException>(async () => await handler.Handle(request, CancellationToken.None));
			Assert.That(ex.Message, Is.EqualTo("User can't have an empty login."));
		}

		[Test]
		public void EditUserCommand_EmptyPassword()
		{
			var mockUserRepository = new Mock<IUserRepository>();

			mockUserRepository
				.Setup(repo => repo.FindAsync(user.Id, default!))
				.ReturnsAsync(user);

			var request = new EditUserCommand(user.Id, login, string.Empty, role);
			var handler = new EditUserCommandHandler(mockUserRepository.Object);

			var ex = Assert.ThrowsAsync<BusinessLogicException>(async () => await handler.Handle(request, CancellationToken.None));
			Assert.That(ex.Message, Is.EqualTo("User can't have an empty password."));
		}
	}
}
