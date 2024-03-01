using MediatR;
using Tavernkeep.Core.Contracts.Character.Dtos;
using Tavernkeep.Core.Entities;

namespace Tavernkeep.Application.UseCases.Characters.Commands.AssignUser
{
    public class AssignUserCommand : IRequest<CharacterDto>
    {
        public Guid CharacterId { get; set; }
        public Guid UserId { get; set; }

        public AssignUserCommand(Guid characterId, Guid userId)
        {
            CharacterId = characterId;
            UserId = userId;
        }
    }
}
