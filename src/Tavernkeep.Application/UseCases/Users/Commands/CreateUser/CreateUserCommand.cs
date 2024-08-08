using MediatR;
using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Entities;

namespace Tavernkeep.Application.UseCases.Users.Commands.CreateUser
{
	public class CreateUserCommand(string login, string password, UserRole role, bool initializeCharacter, string? characterName = null) : IRequest<User>
	{
		public string Login { get; set; } = login;
		public string Password { get; set; } = password;
		public UserRole Role { get; set; } = role;
		public bool InitializeCharacter { get; set; } = initializeCharacter;
		public string? CharacterName { get; set; } = characterName;
	}
}
