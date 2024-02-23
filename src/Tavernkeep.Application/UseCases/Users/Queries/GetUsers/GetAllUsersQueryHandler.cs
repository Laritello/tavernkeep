using MediatR;
using Tavernkeep.Core.Entities;
using Tavernkeep.Core.Repositories;

namespace Tavernkeep.Application.Actions.Users.Queries.GetUsers
{
    public class GetAllUsersQueryHandler(IUserRepository repository) : IRequestHandler<GetAllUsersQuery, List<User>>
    {
        public async Task<List<User>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await repository.GetAllUsersAsync(cancellationToken);
            return users;
        }
    }
}
