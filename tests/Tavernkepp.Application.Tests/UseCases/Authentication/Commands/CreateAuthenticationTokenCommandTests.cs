using Moq;
using Tavernkeep.Application.Interfaces;
using Tavernkeep.Application.UseCases.Authentication.Commands.CreateAuthenticationToken;
using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Entities;
using Tavernkeep.Core.Exceptions;
using Tavernkeep.Core.Repositories;

namespace Tavernkepp.Application.Tests.UseCases.Authentication.Commands
{
	public class CreateAuthenticationTokenCommandTests
	{
		private readonly string correctLogin = "default_user";
		private readonly string correctPassword = "default_password";

		private readonly string wrongLogin = "wrong_user";
		private readonly string wrongPassword = "wrong_password";

		private readonly string token = "default_token";
		private readonly string refreshToken = "default_refresh_token";

		private readonly User authorizedUser;

		public CreateAuthenticationTokenCommandTests()
		{
			authorizedUser = new(correctLogin, correctPassword, UserRole.Player);
		}

		[Test]
		public async Task CreateAuthenticationTokenCommand_Success()
		{
			var mockUserRepository = new Mock<IUserRepository>();
			var mockTokenRepository = new Mock<IRefreshTokenRepository>();
			var mockAuthTokenService = new Mock<IAuthTokenService>();

			mockUserRepository.Setup(repo => repo.GetUserByLoginAsync(authorizedUser.Login, CancellationToken.None)).ReturnsAsync(authorizedUser);
			mockAuthTokenService.Setup(service => service.GenerateAccessToken(authorizedUser)).Returns(token);
			mockAuthTokenService.Setup(service => service.GenerateRefreshToken()).Returns(refreshToken);

			var request = new CreateAuthenticationTokenCommand(correctLogin, correctPassword);
			var handler = new CreateAuthenticationTokenCommandHandler(mockUserRepository.Object, mockTokenRepository.Object, mockAuthTokenService.Object);

			var response = await handler.Handle(request, CancellationToken.None);

			Assert.Multiple(() =>
			{
				Assert.That(response.AccessToken, Is.EqualTo(token));
				Assert.That(response.RefreshToken, Is.EqualTo(refreshToken));
			});
		}

		[Test]
		public void CreateAuthenticationTokenCommand_WrongLogin()
		{
			var mockUserRepository = new Mock<IUserRepository>();
			var mockTokenRepository = new Mock<IRefreshTokenRepository>();
			var mockAuthTokenService = new Mock<IAuthTokenService>();

			mockUserRepository.Setup(repo => repo.GetUserByLoginAsync(authorizedUser.Login, CancellationToken.None)).ReturnsAsync(authorizedUser);
			mockAuthTokenService.Setup(service => service.GenerateAccessToken(authorizedUser)).Returns(token);
			mockAuthTokenService.Setup(service => service.GenerateRefreshToken()).Returns(refreshToken);

			var request = new CreateAuthenticationTokenCommand(string.Empty, correctPassword);
			var handler = new CreateAuthenticationTokenCommandHandler(mockUserRepository.Object, mockTokenRepository.Object, mockAuthTokenService.Object);

			Assert.ThatAsync(async () => await handler.Handle(request, CancellationToken.None),
				Throws.TypeOf<BusinessLogicException>()
				.With.Message.EqualTo("No user login provided."));
		}

		[Test]
		public void CreateAuthenticationTokenCommand_UserNotFound()
		{
			var mockUserRepository = new Mock<IUserRepository>();
			var mockTokenRepository = new Mock<IRefreshTokenRepository>();
			var mockAuthTokenService = new Mock<IAuthTokenService>();

			mockUserRepository.Setup(repo => repo.GetUserByLoginAsync(authorizedUser.Login, CancellationToken.None)).ReturnsAsync(authorizedUser);
			mockAuthTokenService.Setup(service => service.GenerateAccessToken(authorizedUser)).Returns(token);
			mockAuthTokenService.Setup(service => service.GenerateRefreshToken()).Returns(refreshToken);

			var request = new CreateAuthenticationTokenCommand(wrongLogin, correctPassword);
			var handler = new CreateAuthenticationTokenCommandHandler(mockUserRepository.Object, mockTokenRepository.Object, mockAuthTokenService.Object);

			Assert.ThatAsync(async () => await handler.Handle(request, CancellationToken.None),
				Throws.TypeOf<BusinessLogicException>()
				.With.Message.EqualTo("User with provided login not found."));
		}

		[Test]
		public void CreateAuthenticationTokenCommand_WrongPassword()
		{
			var mockUserRepository = new Mock<IUserRepository>();
			var mockTokenRepository = new Mock<IRefreshTokenRepository>();
			var mockAuthTokenService = new Mock<IAuthTokenService>();

			mockUserRepository.Setup(repo => repo.GetUserByLoginAsync(authorizedUser.Login, CancellationToken.None)).ReturnsAsync(authorizedUser);
			mockAuthTokenService.Setup(service => service.GenerateAccessToken(authorizedUser)).Returns(token);
			mockAuthTokenService.Setup(service => service.GenerateRefreshToken()).Returns(refreshToken);

			var request = new CreateAuthenticationTokenCommand(correctLogin, wrongPassword);
			var handler = new CreateAuthenticationTokenCommandHandler(mockUserRepository.Object, mockTokenRepository.Object, mockAuthTokenService.Object);

			Assert.ThatAsync(async () => await handler.Handle(request, CancellationToken.None),
				Throws.TypeOf<BusinessLogicException>()
				.With.Message.EqualTo("Passwords do not match."));
		}
	}
}
