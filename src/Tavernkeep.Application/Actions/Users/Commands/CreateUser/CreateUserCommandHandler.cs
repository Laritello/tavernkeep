using MediatR;
using Tavernkeep.Core.Entities;
using Tavernkeep.Core.Repositories;

namespace Tavernkeep.Application.Actions.Users.Commands.CreateUser
{
    public class CreateUserCommandHandler(IUserRepository repository) : IRequestHandler<CreateUserCommand, User>
    {
        public async Task<User> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            User user = new(request.Login, request.Password);
            
            repository.Save(user);
            await repository.CommitAsync(cancellationToken);

            return user;
        }
    }
}
