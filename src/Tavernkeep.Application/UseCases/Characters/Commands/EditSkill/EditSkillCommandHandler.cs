using MediatR;
using Tavernkeep.Application.Interfaces;
using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Entities;
using Tavernkeep.Core.Exceptions;
using Tavernkeep.Core.Repositories;
using Tavernkeep.Infrastructure.Notifications.Notifications;

namespace Tavernkeep.Application.UseCases.Characters.Commands.EditSkill
{
    public class EditSkillCommandHandler(
        IUserRepository userRepository,
        ICharacterRepository characterRepository,
        INotificationService notificationService
        ) : IRequestHandler<EditSkillCommand, Skill>
    {
        public async Task<Skill> Handle(EditSkillCommand request, CancellationToken cancellationToken)
        {
            var initiator = await userRepository.FindAsync(request.InitiatorId)
                ?? throw new BusinessLogicException("User with specified ID doesn't exist.");

            var character = await characterRepository.GetFullCharacterAsync(request.CharacterId, cancellationToken)
                ?? throw new BusinessLogicException("Character with specified ID doesn't exist.");

            if (character.Owner.Id != request.InitiatorId && initiator.Role != UserRole.Master)
                throw new InsufficientPermissionException("You do not have the necessary permissions to perform this operation.");

            var skill = character.GetSkill(request.Type);
            skill.Proficiency = request.Proficiency;

            characterRepository.Save(character);
            await characterRepository.CommitAsync(cancellationToken);
            await notificationService.QueueSkillNotification(new SkillEditedNotification()
            {
                CharacterId = character.Id,
                Type = skill.Type,
                Proficiency = skill.Proficiency
            });

            return skill;
        }
    }
}
