using MediatR;
using Tavernkeep.Core.Entities;

namespace Tavernkeep.Application.Actions.Users.Queries.GetUsers
{
    public class GetAllUsersQuery : IRequest<List<User>> { }
}
