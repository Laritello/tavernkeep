using MediatR;
using Tavernkeep.Core.Contracts.Authentication.Responses;

namespace Tavernkeep.Application.Actions.Authentication.Commands.CreateAuthenticationnToken
{
    public class CreateAuthenticationTokenCommand : IRequest<AuthenticationResponse>
    {
        public string Login { get; set; }
        public string Password { get; set; }

        public CreateAuthenticationTokenCommand(string login, string password)
        {
            Login = login;
            Password = password;
        }
    }
}
