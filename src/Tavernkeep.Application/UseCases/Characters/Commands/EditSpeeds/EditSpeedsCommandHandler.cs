using MediatR;
using Tavernkeep.Application.Interfaces;

namespace Tavernkeep.Application.UseCases.Characters.Commands.EditSpeeds
{
	public class EditSpeedsCommandHandler(ICharacterService characterService) : IRequestHandler<EditSpeedsCommand>
	{
		public async Task Handle(EditSpeedsCommand request, CancellationToken cancellationToken)
		{
			var character = await characterService.RetrieveCharacterForEdit(request.CharacterId, request.InitiatorId, cancellationToken);

			foreach (var key in request.Speeds.Keys)
			{
				var speed = character.GetSpeed(key);

				speed.Active = request.Speeds[key].Active;
				speed.Base = request.Speeds[key].Base;
			}

			await characterService.SaveCharacter(character, cancellationToken);
		}
	}
}
