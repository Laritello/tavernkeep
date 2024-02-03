using MediatR;
using Tavernkeep.Core.Entities;

namespace Tavernkeep.Application.Actions.Users.Commands.CreateUser
{
    public class CreateUserCommand : IRequest<User>
    {
        public string Login { get; set; }
        public string Password { get; set; }

        public CreateUserCommand(string login, string password)
        {
            Login = login;
            Password = password;
        }
    }
}
