using MediatR;
using Tavernkeep.Core.Exceptions;
using Tavernkeep.Core.Repositories;

namespace Tavernkeep.Application.UseCases.Users.Commands.DeleteUser
{
	public class DeleteUserCommandHandler(IUserRepository repository) : IRequestHandler<DeleteUserCommand>
	{
		public async Task Handle(DeleteUserCommand request, CancellationToken cancellationToken)
		{
			var user = await repository.FindAsync(request.UserId)
				?? throw new BusinessLogicException("User with specified ID doesn't exist.");

			repository.Remove(user);
			await repository.CommitAsync(cancellationToken);
		}
	}
}
