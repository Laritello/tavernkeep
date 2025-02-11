using MediatR;
using Tavernkeep.Core.Entities;
using Tavernkeep.Core.Exceptions;
using Tavernkeep.Core.Repositories;
using Tavernkeep.Core.Specifications.Users;

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
