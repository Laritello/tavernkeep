using MediatR;
using Tavernkeep.Core.Entities;

namespace Tavernkeep.Application.UseCases.Users.Queries.GetUser
{
	public class GetUserQuery(Guid userId) : IRequest<User>
	{
		public Guid UserId { get; set; } = userId;
	}
}
