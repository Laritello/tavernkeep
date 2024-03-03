using MediatR;
using Tavernkeep.Core.Contracts.Users.Dtos;

namespace Tavernkeep.Application.Actions.Users.Queries.GetUsers
{
    public class GetAllUsersQuery : IRequest<List<UserDto>> { }
}
