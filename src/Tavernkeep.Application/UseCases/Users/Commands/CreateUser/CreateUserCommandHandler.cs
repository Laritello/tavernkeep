using MediatR;
using Tavernkeep.Application.Actions.Characters.Commands.CreateCharacter;
using Tavernkeep.Core.Entities;
using Tavernkeep.Core.Repositories;

namespace Tavernkeep.Application.Actions.Users.Commands.CreateUser
{
    public class CreateUserCommandHandler(IMediator mediator, IUserRepository repository) : IRequestHandler<CreateUserCommand, User>
    {
        public async Task<User> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            User user = new(request.Login, request.Password, request.Role);
            
            repository.Save(user);
            await repository.CommitAsync(cancellationToken);

            if (request.InitializeCharacter)
            {
                var character = await mediator.Send(new CreateCharacterCommand(user.Id, request.CharacterName ?? "Default Character"), cancellationToken);

                user.AddCharacter(character);
                user.ActiveCharacter = character;

                await repository.CommitAsync(cancellationToken);
            }

            return user;
        }
    }
}
