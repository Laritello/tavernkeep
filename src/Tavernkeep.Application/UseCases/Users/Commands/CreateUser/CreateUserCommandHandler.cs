using AutoMapper;
using MediatR;
using Tavernkeep.Core.Contracts.Users.Dtos;
using Tavernkeep.Core.Entities;
using Tavernkeep.Core.Repositories;

namespace Tavernkeep.Application.Actions.Users.Commands.CreateUser
{
    public class CreateUserCommandHandler(IUserRepository repository, IMapper mapper) : IRequestHandler<CreateUserCommand, UserDto>
    {
        public async Task<UserDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            User user = new(request.Login, request.Password, request.Role);
            
            repository.Save(user);
            await repository.CommitAsync(cancellationToken);

            return mapper.Map<UserDto>(user);
        }
    }
}
