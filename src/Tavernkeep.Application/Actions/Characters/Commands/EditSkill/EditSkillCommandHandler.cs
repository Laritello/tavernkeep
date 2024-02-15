using MediatR;
using Tavernkeep.Core.Contracts.Character;
using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Exceptions;
using Tavernkeep.Core.Repositories;

namespace Tavernkeep.Application.Actions.Characters.Commands.EditSkill
{
    public class EditSkillCommandHandler(IUserRepository userRepository, ICharacterRepository characterRepository) : IRequestHandler<EditSkillCommand, Skill>
    {
        public async Task<Skill> Handle(EditSkillCommand request, CancellationToken cancellationToken)
        {
            var initiator = await userRepository.FindAsync(request.InitiatorId) 
                ?? throw new BusinessLogicException("User with specified ID doesn't exist.");

            var character = await characterRepository.GetFullCharacter(request.CharacterId)
                ?? throw new BusinessLogicException("Character with specified ID doesn't exist.");

            // TODO: Throw 403 - forbidden
            if (character.Owner.Id != request.InitiatorId && initiator.Role != UserRole.Master)
                throw new BusinessLogicException("User doesn't have the rights to change other's characters");

            var skill = character.GetSkill(request.Type);
            skill.Proficiency = request.Proficiency;

            characterRepository.Save(character);
            await characterRepository.CommitAsync(cancellationToken);

            return skill;
        }
    }
}
