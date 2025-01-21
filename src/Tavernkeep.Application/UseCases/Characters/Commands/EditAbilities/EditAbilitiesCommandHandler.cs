using MediatR;
using Tavernkeep.Application.Interfaces;

namespace Tavernkeep.Application.UseCases.Characters.Commands.EditAbilities
{
	public class EditAbilitiesCommandHandler(ICharacterService characterService) : IRequestHandler<EditAbilitiesCommand>
	{
		public async Task Handle(EditAbilitiesCommand request, CancellationToken cancellationToken)
		{
			var character = await characterService.RetrieveCharacterForEdit(request.CharacterId, request.InitiatorId, cancellationToken);

			foreach (var key in request.Scores.Keys)
			{
				var ability = character.Abilities[key];
				ability.Score = request.Scores[key];
			}

			await characterService.SaveCharacter(character, cancellationToken);
		}
	}
}
