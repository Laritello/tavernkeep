using MediatR;
using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Entities;

namespace Tavernkeep.Application.UseCases.Users.Commands.EditUser
{
	public class EditUserCommand(Guid userId, string login, string password, UserRole role) : IRequest<User>
	{
		public Guid UserId { get; set; } = userId;
		public string Login { get; set; } = login;
		public string Password { get; set; } = password;
		public UserRole Role { get; set; } = role;
	}
}
