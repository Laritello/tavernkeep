using Moq;
using Tavernkeep.Application.UseCases.Users.Commands.CreateUser;
using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Exceptions;
using Tavernkeep.Core.Repositories;

namespace Tavernkepp.Application.Tests.UseCases.Users.Commands
{
	public class CreateUserCommandTests
	{
		private readonly string login = "login";
		private readonly string password = "password";
		private readonly UserRole role = UserRole.Player;

		public CreateUserCommandTests()
		{
			
		}

		[Test]
		public async Task CreateUserCommand_Success()
		{
			var mockUserRepository = new Mock<IUserRepository>();

			var request = new CreateUserCommand(login, password, role);
			var handler = new CreateUserCommandHandler(mockUserRepository.Object);

			var response = await handler.Handle(request, CancellationToken.None);

			Assert.Multiple(() =>
			{
				Assert.That(response.Login, Is.EqualTo(login));
				Assert.That(response.Password, Is.EqualTo(password));
				Assert.That(response.Role, Is.EqualTo(role));
			});
		}

		[Test]
		public void CreateUserCommand_EmptyLogin()
		{
			var mockUserRepository = new Mock<IUserRepository>();

			var request = new CreateUserCommand(string.Empty, password, UserRole.Player);
			var handler = new CreateUserCommandHandler(mockUserRepository.Object);

			Assert.ThatAsync(async () => await handler.Handle(request, CancellationToken.None),
				Throws.TypeOf<BusinessLogicException>()
				.With.Message.EqualTo("User can't have an empty login."));
		}

		[Test]
		public void CreateUserCommand_EmptyPassword()
		{
			var mockUserRepository = new Mock<IUserRepository>();

			var request = new CreateUserCommand(login, string.Empty, UserRole.Player);
			var handler = new CreateUserCommandHandler(mockUserRepository.Object);

			Assert.ThatAsync(async () => await handler.Handle(request, CancellationToken.None),
				Throws.TypeOf<BusinessLogicException>()
				.With.Message.EqualTo("User can't have an empty password."));
		}
	}
}
