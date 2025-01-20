using MediatR;
using Tavernkeep.Application.Interfaces;
using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Entities.Pathfinder.Properties;
using Tavernkeep.Core.Exceptions;
using Tavernkeep.Core.Repositories;

namespace Tavernkeep.Application.UseCases.Custom.Commands.AddCustomSkill
{
	public class AddCustomSkillCommandHandler(
		IUserRepository userRepository,
		ICharacterRepository characterRepository,
		INotificationService notificationService
		) : IRequestHandler<AddCustomSkillCommand>
	{
		public async Task Handle(AddCustomSkillCommand request, CancellationToken cancellationToken)
		{
			if (request.Type != SkillType.Custom && request.Type != SkillType.Lore)
				throw new BusinessLogicException($"Invalid skill type: {nameof(request.Type)}");

			var initiator = await userRepository.FindAsync(request.InitiatorId, cancellationToken: cancellationToken)
				?? throw new BusinessLogicException("User with specified ID doesn't exist.");

			var character = await characterRepository.GetFullCharacterAsync(request.CharacterId, cancellationToken)
				?? throw new BusinessLogicException("Character with specified ID doesn't exist.");

			if (character.Owner.Id != request.InitiatorId && initiator.Role != UserRole.Master)
				throw new InsufficientPermissionException("You do not have the necessary permissions to perform this operation.");

			if (character.Skills.Any(s => s.Name == request.Name))
				throw new BusinessLogicException("Character already has a skill with this name.");

			if (request.Type == SkillType.Custom && (request.BaseAbility is null || !character.Abilities.Any(a => a.Name == request.BaseAbility)))
				throw new BusinessLogicException("Character does not have an ability with this name.");

			Skill skill = new(request.Name, Proficiency.Untrained, request.Type)
			{
				Owner = character,
				Ability = character.Abilities[request.Type == SkillType.Custom ? request.BaseAbility! : "Intelligence"]
			};

			character.Skills.Add(skill);

			characterRepository.Save(character);
			await characterRepository.CommitAsync(cancellationToken);
			await notificationService.QueueCharacterNotificationAsync(character, cancellationToken);
		}
	}
}
