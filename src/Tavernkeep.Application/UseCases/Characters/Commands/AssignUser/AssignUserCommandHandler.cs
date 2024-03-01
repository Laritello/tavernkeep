using AutoMapper;
using MediatR;
using Tavernkeep.Core.Contracts.Character.Dtos;
using Tavernkeep.Core.Exceptions;
using Tavernkeep.Core.Repositories;

namespace Tavernkeep.Application.UseCases.Characters.Commands.AssignUser
{
    public class AssignUserCommandHandler(
        IUserRepository userRepository, 
        ICharacterRepository characterRepository,
        IMapper mapper
        ) : IRequestHandler<AssignUserCommand, CharacterDto>
    {
        public async Task<CharacterDto> Handle(AssignUserCommand request, CancellationToken cancellationToken)
        {
            var character = await characterRepository.FindAsync(request.CharacterId)
                ?? throw new BusinessLogicException("Character with specified ID doesn't exist.");

            var user = await userRepository.FindAsync(request.UserId)
                ?? throw new BusinessLogicException("User with specified ID doesn't exist.");

            character.Owner = user;
            characterRepository.Save(character);
            await characterRepository.CommitAsync(cancellationToken);
            // TODO: SignalR notification

            return mapper.Map<CharacterDto>(character);
        }
    }
}
