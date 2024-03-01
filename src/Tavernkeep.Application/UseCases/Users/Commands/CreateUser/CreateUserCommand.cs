using MediatR;
using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Contracts.Users.Dtos;
using Tavernkeep.Core.Entities;

namespace Tavernkeep.Application.Actions.Users.Commands.CreateUser
{
    public class CreateUserCommand : IRequest<UserDto>
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public UserRole Role { get; set; }

        public CreateUserCommand(string login, string password, UserRole role = UserRole.Player)
        {
            Login = login;
            Password = password;
            Role = role;
        }
    }
}
