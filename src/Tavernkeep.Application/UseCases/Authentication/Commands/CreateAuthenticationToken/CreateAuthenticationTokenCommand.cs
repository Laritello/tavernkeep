using MediatR;
using Tavernkeep.Core.Contracts.Authentication.Responses;

namespace Tavernkeep.Application.UseCases.Authentication.Commands.CreateAuthenticationToken
{
	public class CreateAuthenticationTokenCommand(string login, string password) : IRequest<AuthenticationResponse>
	{
		public string Login { get; set; } = login;
		public string Password { get; set; } = password;
	}
}
