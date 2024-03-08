using Moq;
using System.Security.Claims;
using Tavernkeep.Application.Interfaces;
using Tavernkeep.Application.UseCases.Authentication.Commands.RefreshAuthenticationToken;
using Tavernkeep.Core.Contracts.Authentication;
using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Entities;
using Tavernkeep.Core.Exceptions;
using Tavernkeep.Core.Repositories;

namespace Tavernkepp.Application.Tests.UseCases.Authentication
{
    public class RefreshAuthenticationTokenTests
    {
        private readonly string login = "default_user";
        private readonly string password = "default_password";
        private readonly string accessToken = "access_token";
        private readonly string refreshToken = "refresh_token";

        private readonly string newAccessToken = "new_access_token";
        private readonly string newRefreshToken = "new_refresh_token";

        private readonly Guid userId = Guid.NewGuid();

        private readonly User user;

        public RefreshAuthenticationTokenTests()
        {
            user = new User(login, password, UserRole.Player) { Id = userId };
        }

        [Test]
        public async Task Authentication_CreateAuthenticationTokenCommand_Success()
        {
            var mockAuthTokenService = new Mock<IAuthTokenService>();
            var mockUserRepository = new Mock<IUserRepository>();
            var mockTokenRepository = new Mock<IRefreshTokenRepository>();

            var claims = new ClaimsIdentity();
            claims.AddClaim(new Claim(JwtCustomClaimNames.UserId, userId.ToString()));

            mockAuthTokenService
                .Setup(service => service.GetUserIdentityFromExpiredToken(accessToken))
                .ReturnsAsync(claims);
            mockAuthTokenService
                .Setup(service => service.GenerateAccessToken(user))
                .Returns(newAccessToken);
            mockAuthTokenService
                .Setup(service => service.GenerateRefreshToken())
                .Returns(newRefreshToken);

            mockUserRepository
                .Setup(repo => repo.FindAsync(userId, null!))
                .ReturnsAsync(user);

            mockTokenRepository
                .Setup(repo => repo.GetTokensForUserAsync(userId, CancellationToken.None))
                .ReturnsAsync([ 
                    new() 
                    { 
                        UserId = userId,
                        Token = refreshToken, 
                        Expires = DateTime.UtcNow.AddDays(7)
                    }]);

            var request = new RefreshAuthenticationTokenCommand(accessToken, refreshToken);
            var handler = new RefreshAuthenticationTokenCommandHandler(mockUserRepository.Object, mockTokenRepository.Object, mockAuthTokenService.Object);

            var response = await handler.Handle(request, CancellationToken.None);

            Assert.Multiple(() =>
            {
                Assert.That(response.AccessToken, Is.EqualTo(newAccessToken));
                Assert.That(response.RefreshToken, Is.EqualTo(newRefreshToken));
            });
        }

        [Test]
        public void Authentication_CreateAuthenticationTokenCommand_WrongUserId()
        {
            var mockAuthTokenService = new Mock<IAuthTokenService>();
            var mockUserRepository = new Mock<IUserRepository>();
            var mockTokenRepository = new Mock<IRefreshTokenRepository>();

            var claims = new ClaimsIdentity();
            claims.AddClaim(new Claim(JwtCustomClaimNames.UserId, Guid.Empty.ToString()));

            mockAuthTokenService
                .Setup(service => service.GetUserIdentityFromExpiredToken(accessToken))
                .ReturnsAsync(claims);
            mockAuthTokenService
                .Setup(service => service.GenerateAccessToken(user))
                .Returns(newAccessToken);
            mockAuthTokenService
                .Setup(service => service.GenerateRefreshToken())
                .Returns(newRefreshToken);

            mockUserRepository
                .Setup(repo => repo.FindAsync(userId, null!))
                .ReturnsAsync(user);

            mockTokenRepository
                .Setup(repo => repo.GetTokensForUserAsync(userId, CancellationToken.None))
                .ReturnsAsync([
                    new()
                    {
                        UserId = userId,
                        Token = refreshToken,
                        Expires = DateTime.UtcNow.AddDays(7)
                    }]);

            var request = new RefreshAuthenticationTokenCommand(accessToken, refreshToken);
            var handler = new RefreshAuthenticationTokenCommandHandler(mockUserRepository.Object, mockTokenRepository.Object, mockAuthTokenService.Object);

            var ex = Assert.ThrowsAsync<BusinessLogicException>(async () => await handler.Handle(request, CancellationToken.None));
            Assert.That(ex.Message, Is.EqualTo("User with specified ID doesn't exist."));
        }

        [Test]
        public void Authentication_CreateAuthenticationTokenCommand_TokenNotFound()
        {
            var mockAuthTokenService = new Mock<IAuthTokenService>();
            var mockUserRepository = new Mock<IUserRepository>();
            var mockTokenRepository = new Mock<IRefreshTokenRepository>();

            var claims = new ClaimsIdentity();
            claims.AddClaim(new Claim(JwtCustomClaimNames.UserId, userId.ToString()));

            mockAuthTokenService
                .Setup(service => service.GetUserIdentityFromExpiredToken(accessToken))
                .ReturnsAsync(claims);
            mockAuthTokenService
                .Setup(service => service.GenerateAccessToken(user))
                .Returns(newAccessToken);
            mockAuthTokenService
                .Setup(service => service.GenerateRefreshToken())
                .Returns(newRefreshToken);

            mockUserRepository
                .Setup(repo => repo.FindAsync(userId, null!))
                .ReturnsAsync(user);

            mockTokenRepository
                .Setup(repo => repo.GetTokensForUserAsync(userId, CancellationToken.None))
                .ReturnsAsync([]);

            var request = new RefreshAuthenticationTokenCommand(accessToken, refreshToken);
            var handler = new RefreshAuthenticationTokenCommandHandler(mockUserRepository.Object, mockTokenRepository.Object, mockAuthTokenService.Object);

            var ex = Assert.ThrowsAsync<BusinessLogicException>(async () => await handler.Handle(request, CancellationToken.None));
            Assert.That(ex.Message, Is.EqualTo("Specified refresh token isn't registered."));
        }

        [Test]
        public void Authentication_CreateAuthenticationTokenCommand_TokenExpired()
        {
            var mockAuthTokenService = new Mock<IAuthTokenService>();
            var mockUserRepository = new Mock<IUserRepository>();
            var mockTokenRepository = new Mock<IRefreshTokenRepository>();

            var claims = new ClaimsIdentity();
            claims.AddClaim(new Claim(JwtCustomClaimNames.UserId, userId.ToString()));

            mockAuthTokenService
                .Setup(service => service.GetUserIdentityFromExpiredToken(accessToken))
                .ReturnsAsync(claims);
            mockAuthTokenService
                .Setup(service => service.GenerateAccessToken(user))
                .Returns(newAccessToken);
            mockAuthTokenService
                .Setup(service => service.GenerateRefreshToken())
                .Returns(newRefreshToken);

            mockUserRepository
                .Setup(repo => repo.FindAsync(userId, null!))
                .ReturnsAsync(user);

            mockTokenRepository
                .Setup(repo => repo.GetTokensForUserAsync(userId, CancellationToken.None))
                .ReturnsAsync([
                    new()
                    {
                        UserId = userId,
                        Token = refreshToken,
                        Expires = DateTime.UtcNow.AddDays(-7)
                    }]);

            var request = new RefreshAuthenticationTokenCommand(accessToken, refreshToken);
            var handler = new RefreshAuthenticationTokenCommandHandler(mockUserRepository.Object, mockTokenRepository.Object, mockAuthTokenService.Object);

            var ex = Assert.ThrowsAsync<BusinessLogicException>(async () => await handler.Handle(request, CancellationToken.None));
            Assert.That(ex.Message, Is.EqualTo("Expired refresh token provided."));
        }
    }
}
