using MediatR;
using Tavernkeep.Application.Interfaces;

namespace Tavernkeep.Application.UseCases.Characters.Commands.EditSkills
{
	public class EditSkillsCommandHandler(ICharacterService characterService) : IRequestHandler<EditSkillsCommand>
	{
		public async Task Handle(EditSkillsCommand request, CancellationToken cancellationToken)
		{
			var character = await characterService.RetrieveCharacterForEdit(request.CharacterId, request.InitiatorId, cancellationToken);

			foreach (var key in request.Proficiencies.Keys)
			{
				var skill = character.Skills[key];
				skill.Proficiency = request.Proficiencies[key];
			}

			await characterService.SaveCharacter(character, cancellationToken);
		}
	}
}
