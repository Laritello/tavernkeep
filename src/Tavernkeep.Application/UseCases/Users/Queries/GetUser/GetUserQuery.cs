using MediatR;
using Tavernkeep.Core.Contracts.Users.Dtos;
using Tavernkeep.Core.Entities;

namespace Tavernkeep.Application.UseCases.Users.Queries.GetCurrentUser
{
    public class GetUserQuery : IRequest<UserDto>
    {
        public Guid UserId { get; set; }

        public GetUserQuery(Guid userId)
        {
            UserId = userId;
        }
    }
}
