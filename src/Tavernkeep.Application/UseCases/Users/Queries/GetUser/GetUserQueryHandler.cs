using MediatR;
using Tavernkeep.Domain.Entities;
using Tavernkeep.Domain.Exceptions;
using Tavernkeep.Domain.Repositories;
using Tavernkeep.Domain.Specifications.Users;

namespace Tavernkeep.Application.UseCases.Users.Queries.GetUser
{
	public class GetUserQueryHandler(IUserRepository userRepository) : IRequestHandler<GetUserQuery, User>
	{
		public async Task<User> Handle(GetUserQuery request, CancellationToken cancellationToken)
		{
			var user = await userRepository.FindAsync(new UserFullSpecification(request.UserId), cancellationToken)
				?? throw new BusinessLogicException("User with specified ID not found.");

			return user;
		}
	}
}
