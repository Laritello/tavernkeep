using MediatR;
using Tavernkeep.Domain.Contracts.Enums;
using Tavernkeep.Domain.Entities;

namespace Tavernkeep.Application.UseCases.Users.Commands.CreateUser
{
	public class CreateUserCommand(string login, string password, UserRole role) : IRequest<User>
	{
		public string Login { get; set; } = login;
		public string Password { get; set; } = password;
		public UserRole Role { get; set; } = role;
	}
}
