using AutoMapper;
using MediatR;
using Tavernkeep.Core.Contracts.Users.Dtos;
using Tavernkeep.Core.Exceptions;
using Tavernkeep.Core.Repositories;
using Tavernkeep.Core.Specifications.Users;

namespace Tavernkeep.Application.UseCases.Users.Queries.GetCurrentUser
{
    public class GetUserQueryHandler(IUserRepository userRepository, IMapper mapper) : IRequestHandler<GetUserQuery, UserDto>
    {
        public async Task<UserDto> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var user = await userRepository.FindAsync(new FullUserSpecification(request.UserId))
                ?? throw new BusinessLogicException("User with specified id not found.");

            return mapper.Map<UserDto>(user);
        }
    }
}
