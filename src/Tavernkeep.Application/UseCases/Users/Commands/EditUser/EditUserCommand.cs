using MediatR;
using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Contracts.Users.Dtos;
using Tavernkeep.Core.Entities;

namespace Tavernkeep.Application.UseCases.Users.Commands.EditUser
{
    public class EditUserCommand : IRequest<UserDto>
    {
        public Guid UserId { get; set; } = default;
        public string Login { get; set; } = default!;
        public string Password { get; set; } = default!;
        public UserRole Role { get; set; } = default!;

        public EditUserCommand(Guid userId, string login, string password, UserRole role)
        {
            UserId = userId;
            Login = login;
            Password = password;
            Role = role;
        }
    }
}
