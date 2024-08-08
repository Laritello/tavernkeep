using MediatR;
using Tavernkeep.Core.Contracts.Authentication.Responses;

namespace Tavernkeep.Application.UseCases.Authentication.Commands.RefreshAuthenticationToken
{
	public class RefreshAuthenticationTokenCommand : IRequest<AuthenticationResponse>
	{
		public string AccessToken { get; set; }
		public string RefreshToken { get; set; }

		public RefreshAuthenticationTokenCommand(string token, string refreshToken)
		{
			AccessToken = token;
			RefreshToken = refreshToken;
		}
	}
}
