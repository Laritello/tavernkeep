using MediatR;
using Tavernkeep.Application.Interfaces;
using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Entities.Pathfinder.Properties;
using Tavernkeep.Core.Exceptions;

namespace Tavernkeep.Application.UseCases.Custom.Commands.EditCustomSkill
{
	public class EditCustomSkillCommandHandler(ICharacterService characterService) : IRequestHandler<EditCustomSkillCommand>
	{
		public async Task Handle(EditCustomSkillCommand request, CancellationToken cancellationToken)
		{
			var character = await characterService.RetrieveCharacterForAction(request.CharacterId, request.InitiatorId, cancellationToken);

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

			await characterService.SaveCharacter(character, cancellationToken);
		}
	}
}
