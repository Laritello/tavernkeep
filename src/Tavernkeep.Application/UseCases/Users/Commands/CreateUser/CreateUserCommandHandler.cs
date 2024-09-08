using MediatR;
using Tavernkeep.Core.Entities;
using Tavernkeep.Core.Exceptions;
using Tavernkeep.Core.Repositories;

namespace Tavernkeep.Application.UseCases.Users.Commands.CreateUser
{
	public class CreateUserCommandHandler(IUserRepository repository) : IRequestHandler<CreateUserCommand, User>
	{
		public async Task<User> Handle(CreateUserCommand request, CancellationToken cancellationToken)
		{
			if (string.IsNullOrWhiteSpace(request.Login))
				throw new BusinessLogicException("User can't have an empty login.");

			if (string.IsNullOrWhiteSpace(request.Password))
				throw new BusinessLogicException("User can't have an empty password.");

			User user = new(request.Login, request.Password, request.Role);

			repository.Save(user);
			await repository.CommitAsync(cancellationToken);

			return user;
		}
	}
}
