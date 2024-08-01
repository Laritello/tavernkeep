using MediatR;
using Tavernkeep.Core.Entities;
using Tavernkeep.Core.Repositories;

namespace Tavernkeep.Application.UseCases.Users.Queries.GetUsers
{
    public class GetAllUsersQueryHandler(IUserRepository repository) : IRequestHandler<GetAllUsersQuery, Dictionary<Guid,User>>
    {
        public async Task<Dictionary<Guid, User>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await repository.GetAllUsersAsync(cancellationToken);
            return users.ToDictionary(x => x.Id);
        }
    }
}
