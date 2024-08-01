using MediatR;

namespace Tavernkeep.Application.UseCases.Users.Commands.DeleteUser
{
    public class DeleteUserCommand(Guid userId) : IRequest
    {
        public Guid UserId { get; set; } = userId;
    }
}
