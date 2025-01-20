using MediatR;
using Tavernkeep.Application.Interfaces;
using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Exceptions;
using Tavernkeep.Core.Repositories;

namespace Tavernkeep.Application.UseCases.Custom.Commands.DeleteCustomSkill
{
	public class DeleteCustomSkillCommandHandler(
		IUserRepository userRepository,
		ICharacterRepository characterRepository,
		INotificationService notificationService
		) : IRequestHandler<DeleteCustomSkillCommand>
	{
		public async Task Handle(DeleteCustomSkillCommand request, CancellationToken cancellationToken)
		{
			var initiator = await userRepository.FindAsync(request.InitiatorId, cancellationToken: cancellationToken)
				?? throw new BusinessLogicException("User with specified ID doesn't exist.");

			var character = await characterRepository.GetFullCharacterAsync(request.CharacterId, cancellationToken)
				?? throw new BusinessLogicException("Character with specified ID doesn't exist.");

			if (character.Owner.Id != request.InitiatorId && initiator.Role != UserRole.Master)
				throw new InsufficientPermissionException("You do not have the necessary permissions to perform this operation.");

			var skill = character.Skills.FirstOrDefault(s => s.Name == request.Name)
				?? throw new BusinessLogicException("Character does not have a skill with this name.");

			if (skill.Type != SkillType.Custom && skill.Type != SkillType.Lore)
				throw new BusinessLogicException("Deteled skill must either have custom or lore type.");

			character.Skills.Remove(skill);

			characterRepository.Save(character);
			await characterRepository.CommitAsync(cancellationToken);
			await notificationService.QueueCharacterNotificationAsync(character, cancellationToken);
		}
	}
}
