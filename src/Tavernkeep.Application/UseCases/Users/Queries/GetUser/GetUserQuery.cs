using MediatR;
using Tavernkeep.Core.Entities;

namespace Tavernkeep.Application.UseCases.Users.Queries.GetUser
{
    public class GetUserQuery : IRequest<User>
    {
        public Guid UserId { get; set; }

        public GetUserQuery(Guid userId)
        {
            UserId = userId;
        }
    }
}
