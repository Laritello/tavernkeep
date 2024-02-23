using MediatR;

namespace Tavernkeep.Application.Actions.Authentication.Commands.CreateAuthenticationnToken
{
    public class CreateAuthenticationTokenCommand : IRequest<string>
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
