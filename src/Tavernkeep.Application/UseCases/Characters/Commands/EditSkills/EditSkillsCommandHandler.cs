using MediatR;
using Tavernkeep.Application.Interfaces;

namespace Tavernkeep.Application.UseCases.Characters.Commands.EditSkills
{
	public class EditSkillsCommandHandler(ICharacterService characterService) : IRequestHandler<EditSkillsCommand>
	{
		public async Task Handle(EditSkillsCommand request, CancellationToken cancellationToken)
		{
			var character = await characterService.RetrieveCharacterForEdit(request.CharacterId, request.InitiatorId, cancellationToken);

			foreach (var key in request.Skills.Keys)
			{
				var skill = character.Skills[key];
				var update = request.Skills[key];

				skill.Proficiency = update.Proficiency ?? skill.Proficiency;
				skill.Pinned = update.Pinned ?? skill.Pinned;
			}

			await characterService.SaveCharacter(character, cancellationToken);
		}
	}
}
