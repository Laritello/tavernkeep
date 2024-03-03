using AutoMapper;
using MediatR;
using Tavernkeep.Core.Contracts.Users.Dtos;
using Tavernkeep.Core.Repositories;

namespace Tavernkeep.Application.Actions.Users.Queries.GetUsers
{
    public class GetAllUsersQueryHandler(IUserRepository repository, IMapper mapper) : IRequestHandler<GetAllUsersQuery, List<UserDto>>
    {
        public async Task<List<UserDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await repository.GetAllUsersAsync(cancellationToken);
            return mapper.Map<List<UserDto>>(users);
        }
    }
}
