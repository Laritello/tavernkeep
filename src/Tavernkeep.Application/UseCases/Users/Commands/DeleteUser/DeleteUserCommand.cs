using MediatR;

namespace Tavernkeep.Application.Actions.Users.Commands.DeleteUser
{
    public class DeleteUserCommand(Guid userId) : IRequest
    {
        public Guid UserId { get; set; } = userId;
    }
}
