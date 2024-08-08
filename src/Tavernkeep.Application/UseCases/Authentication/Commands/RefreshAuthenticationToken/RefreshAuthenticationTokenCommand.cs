using MediatR;
using Tavernkeep.Core.Contracts.Authentication.Responses;

namespace Tavernkeep.Application.UseCases.Authentication.Commands.RefreshAuthenticationToken
{
	public class RefreshAuthenticationTokenCommand(string token, string refreshToken) : IRequest<AuthenticationResponse>
	{
		public string AccessToken { get; set; } = token;
		public string RefreshToken { get; set; } = refreshToken;
	}
}
