using MediatR;
using Tavernkeep.Application.Interfaces;
using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Entities.Pathfinder.Properties;
using Tavernkeep.Core.Exceptions;
using Tavernkeep.Core.Repositories;

namespace Tavernkeep.Application.UseCases.Custom.Commands.EditCustomSkill
{
	public class EditCustomSkillCommandHandler(
		IUserRepository userRepository,
		ICharacterRepository characterRepository,
		INotificationService notificationService
		) : IRequestHandler<EditCustomSkillCommand>
	{
		public async Task Handle(EditCustomSkillCommand request, CancellationToken cancellationToken)
		{
			var initiator = await userRepository.FindAsync(request.InitiatorId, cancellationToken: cancellationToken)
				?? throw new BusinessLogicException("User with specified ID doesn't exist.");

			var character = await characterRepository.GetFullCharacterAsync(request.CharacterId, cancellationToken)
				?? throw new BusinessLogicException("Character with specified ID doesn't exist.");

			if (character.Owner.Id != request.InitiatorId && initiator.Role != UserRole.Master)
				throw new InsufficientPermissionException("You do not have the necessary permissions to perform this operation.");

			var skill = character.Skills.FirstOrDefault(s => s.Name == request.OldName)
				?? throw new BusinessLogicException("Character does not have a skill with this name.");

			if (skill.Type != SkillType.Custom && skill.Type != SkillType.Lore)
				throw new BusinessLogicException("Edited skill must either have custom or lore type.");

			Skill updated = new(request.NewName, skill.Proficiency, skill.Type)
			{
				Owner = skill.Owner,
				Ability = skill.Ability,
			};

			character.Skills.Remove(skill);
			character.Skills.Add(updated);

			characterRepository.Save(character);
			await characterRepository.CommitAsync(cancellationToken);
			await notificationService.QueueCharacterNotificationAsync(character, cancellationToken);
		}
	}
}
