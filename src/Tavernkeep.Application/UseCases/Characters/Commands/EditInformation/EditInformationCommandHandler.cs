using MediatR;
using Tavernkeep.Application.Interfaces;

namespace Tavernkeep.Application.UseCases.Characters.Commands.EditInformation
{
	public class EditInformationCommandHandler(ICharacterService characterService) : IRequestHandler<EditInformationCommand>
	{
		public async Task Handle(EditInformationCommand request, CancellationToken cancellationToken)
		{
			var character = await characterService.RetrieveCharacterForAction(request.CharacterId, request.InitiatorId, cancellationToken);

			character.Name = request.Information.Name;
			character.Ancestry.Name = request.Information.Ancestry;
			character.Class.Name = request.Information.Class;
			character.Level = request.Information.Level;

			await characterService.SaveCharacter(character, cancellationToken);
		}
	}
}
