using MediatR;
using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Entities;

namespace Tavernkeep.Application.Actions.Users.Commands.CreateUser
{
    public class CreateUserCommand : IRequest<User>
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public UserRole Role { get; set; }
        public bool InitializeCharacter { get; set; }
        public string? CharacterName { get; set; }

        public CreateUserCommand(string login, string password, UserRole role, bool initializeCharacter, string? characterName = null)
        {
            Login = login;
            Password = password;
            Role = role;
            InitializeCharacter = initializeCharacter;
            CharacterName = characterName;
        }
    }
}
