using Moq;
using Tavernkeep.Application.Actions.Authentication.Commands.CreateAuthenticationnToken;
using Tavernkeep.Application.Interfaces;
using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Entities;
using Tavernkeep.Core.Exceptions;
using Tavernkeep.Core.Repositories;

namespace Tavernkepp.Application.Tests.UseCases.Authentication
{
    public class AuthenticationTests
    {
        private readonly Mock<IUserRepository> _mockUserRepository;
        private readonly Mock<IRefreshTokenRepository> _mockTokenRepository;
        private readonly Mock<IAuthTokenService> _mockAuthTokenService;

        private const string _correctLogin = "default_user";
        private const string _correctPassword = "default_password";

        private const string _wrongLogin = "wrong_user";
        private const string _wrongPassword = "wrong_password";

        private const string _token = "default_token";
        private const string _refreshToken = "default_refresh_token";

        private readonly User authorizedUser = new(_correctLogin, _correctPassword, UserRole.Player);

        public AuthenticationTests()
        {
            _mockUserRepository = new Mock<IUserRepository>();
            _mockUserRepository.Setup(repo => repo.GetUserByLoginAsync(authorizedUser.Login, CancellationToken.None)).ReturnsAsync(authorizedUser);

            _mockTokenRepository = new Mock<IRefreshTokenRepository>();

            _mockAuthTokenService = new Mock<IAuthTokenService>();
            _mockAuthTokenService.Setup(service => service.GenerateAccessToken(authorizedUser)).Returns(_token);
            _mockAuthTokenService.Setup(service => service.GenerateRefreshToken()).Returns(_refreshToken);
        }

        [Test]
        public async Task Authentication_CreateAuthenticationTokenCommand_ReturnsToken()
        {
            var request = new CreateAuthenticationTokenCommand(_correctLogin, _correctPassword);
            var handler = new CreateAuthenticationTokenCommandHandler(_mockUserRepository.Object, _mockTokenRepository.Object, _mockAuthTokenService.Object);

            var response = await handler.Handle(request, CancellationToken.None);

            Assert.Multiple(() =>
            {
                Assert.That(response.AccessToken, Is.EqualTo(_token));
                Assert.That(response.RefreshToken, Is.EqualTo(_refreshToken));
            });
        }

        [Test]
        public void Authenctication_CreateAuthenticationTokenCommand_NoLogin()
        {
            var request = new CreateAuthenticationTokenCommand(string.Empty, _correctPassword);
            var handler = new CreateAuthenticationTokenCommandHandler(_mockUserRepository.Object, _mockTokenRepository.Object, _mockAuthTokenService.Object);

            var ex = Assert.ThrowsAsync<BusinessLogicException>(async () => await handler.Handle(request, CancellationToken.None));
            Assert.That(ex.Message, Is.EqualTo("No user login provided."));
        }

        [Test]
        public void Authentication_CreateAuthenticationTokenCommand_UserNotFound()
        {
            var request = new CreateAuthenticationTokenCommand(_wrongLogin, _correctPassword);
            var handler = new CreateAuthenticationTokenCommandHandler(_mockUserRepository.Object, _mockTokenRepository.Object, _mockAuthTokenService.Object);

            var ex = Assert.ThrowsAsync<BusinessLogicException>(async () => await handler.Handle(request, CancellationToken.None));
            Assert.That(ex.Message, Is.EqualTo("User with provided login not found."));
        }

        [Test]
        public void Authentication_CreateAuthenticationTokenCommand_WrongPassword()
        {
            var request = new CreateAuthenticationTokenCommand(_correctLogin, _wrongPassword);
            var handler = new CreateAuthenticationTokenCommandHandler(_mockUserRepository.Object, _mockTokenRepository.Object, _mockAuthTokenService.Object);

            var ex = Assert.ThrowsAsync<BusinessLogicException>(async () => await handler.Handle(request, CancellationToken.None));
            Assert.That(ex.Message, Is.EqualTo("Passwords do not match."));
        }
    }
}
