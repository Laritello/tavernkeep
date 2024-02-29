using AutoMapper;
using MediatR;
using Tavernkeep.Core.Contracts.Users.Dtos;
using Tavernkeep.Core.Entities;
using Tavernkeep.Core.Exceptions;
using Tavernkeep.Core.Repositories;

namespace Tavernkeep.Application.UseCases.Users.Commands.EditUser
{
    public class EditUserCommandHandler(IUserRepository userRepository, IMapper mapper) : IRequestHandler<EditUserCommand, UserDto>
    {
        public async Task<UserDto> Handle(EditUserCommand request, CancellationToken cancellationToken)
        {
            var user = await userRepository.FindAsync(request.UserId)
                ?? throw new BusinessLogicException("User with specified ID doesn't exist.");

            user.Login = request.Login;
            user.Password = request.Password;
            user.Role = request.Role;

            userRepository.Save(user);
            await userRepository.CommitAsync(cancellationToken);
            // TODO: Send notification

            return mapper.Map<UserDto>(user);
        }
    }
}
