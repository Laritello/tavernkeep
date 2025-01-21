using MediatR;
using Tavernkeep.Application.Interfaces;

namespace Tavernkeep.Application.UseCases.Characters.Commands.EditSavingThrows
{
	public class EditSavingThrowsCommandHandler(ICharacterService characterService) : IRequestHandler<EditSavingThrowsCommand>
	{
		public async Task Handle(EditSavingThrowsCommand request, CancellationToken cancellationToken)
		{
			var character = await characterService.RetrieveCharacterForEdit(request.CharacterId, request.InitiatorId, cancellationToken);

			foreach (var key in request.Proficiencies.Keys)
			{
				var savingThrow = character.Skills[key];
				savingThrow.Proficiency = request.Proficiencies[key];
			}

			await characterService.SaveCharacter(character, cancellationToken);
		}
	}
}
