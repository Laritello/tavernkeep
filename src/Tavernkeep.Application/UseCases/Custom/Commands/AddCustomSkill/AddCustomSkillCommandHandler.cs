using MediatR;
using Tavernkeep.Application.Interfaces;
using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Exceptions;

namespace Tavernkeep.Application.UseCases.Custom.Commands.AddCustomSkill
{
	public class AddCustomSkillCommandHandler(ICharacterService characterService) : IRequestHandler<AddCustomSkillCommand>
	{
		public async Task Handle(AddCustomSkillCommand request, CancellationToken cancellationToken)
		{
			if (request.Type != SkillType.Custom && request.Type != SkillType.Lore)
				throw new BusinessLogicException($"Invalid skill type: {nameof(request.Type)}");

			var character = await characterService.RetrieveCharacterForAction(request.CharacterId, request.InitiatorId, cancellationToken);

			if (character.Skills.Any(s => s.Name == request.Name))
				throw new BusinessLogicException("Character already has a skill with this name.");

			if (request.Type == SkillType.Custom && (request.BaseAbility is null || !character.Abilities.Any(a => a.Name == request.BaseAbility)))
				throw new BusinessLogicException("Character does not have an ability with this name.");

			character.Skills.Add
			(
				new(request.Name, Proficiency.Untrained, request.Type)
				{
					Owner = character,
					Ability = character.Abilities[request.Type == SkillType.Custom ? request.BaseAbility! : "Intelligence"]
				}
			);

			await characterService.SaveCharacter(character, cancellationToken);
		}
	}
}
