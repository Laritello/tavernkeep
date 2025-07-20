using MediatR;
using Tavernkeep.Domain.Entities;

namespace Tavernkeep.Application.UseCases.Users.Queries.GetUsers
{
	public class GetAllUsersQuery : IRequest<Dictionary<Guid, User>> { }
}
