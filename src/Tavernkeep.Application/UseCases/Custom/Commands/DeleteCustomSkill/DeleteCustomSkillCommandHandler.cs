using MediatR;
using Tavernkeep.Application.Interfaces;
using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Exceptions;

namespace Tavernkeep.Application.UseCases.Custom.Commands.DeleteCustomSkill
{
	public class DeleteCustomSkillCommandHandler(ICharacterService characterService) : IRequestHandler<DeleteCustomSkillCommand>
	{
		public async Task Handle(DeleteCustomSkillCommand request, CancellationToken cancellationToken)
		{
			var character = await characterService.RetrieveCharacterForEdit(request.CharacterId, request.InitiatorId, cancellationToken);

			var skill = character.Skills.FirstOrDefault(s => s.Name == request.Name)
				?? throw new BusinessLogicException("Character does not have a skill with this name.");

			if (skill.Type != SkillType.Custom && skill.Type != SkillType.Lore)
				throw new BusinessLogicException("Deteled skill must either have custom or lore type.");

			character.Skills.Remove(skill);

			await characterService.SaveCharacter(character, cancellationToken);
		}
	}
}
