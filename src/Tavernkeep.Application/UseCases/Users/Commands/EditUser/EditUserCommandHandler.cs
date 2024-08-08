using MediatR;
using Tavernkeep.Core.Entities;
using Tavernkeep.Core.Exceptions;
using Tavernkeep.Core.Repositories;

namespace Tavernkeep.Application.UseCases.Users.Commands.EditUser
{
	public class EditUserCommandHandler(IUserRepository userRepository) : IRequestHandler<EditUserCommand, User>
	{
		public async Task<User> Handle(EditUserCommand request, CancellationToken cancellationToken)
		{
			var user = await userRepository.FindAsync(request.UserId, cancellationToken: cancellationToken)
				?? throw new BusinessLogicException("User with specified ID doesn't exist.");

			if (string.IsNullOrWhiteSpace(request.Login))
				throw new BusinessLogicException("User can't have an empty login.");

			if (string.IsNullOrWhiteSpace(request.Password))
				throw new BusinessLogicException("User can't have an empty password.");

			user.Login = request.Login;
			user.Password = request.Password;
			user.Role = request.Role;

			userRepository.Save(user);
			await userRepository.CommitAsync(cancellationToken);
			// TODO: Send notification

			return user;
		}
	}
}
