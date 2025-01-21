using MediatR;
using Tavernkeep.Application.Interfaces;

namespace Tavernkeep.Application.UseCases.Characters.Commands.EditPerception
{
	public class EditPerceptionCommandHandler(ICharacterService characterService) : IRequestHandler<EditPerceptionCommand>
	{
		public async Task Handle(EditPerceptionCommand request, CancellationToken cancellationToken)
		{
			var character = await characterService.RetrieveCharacterForEdit(request.CharacterId, request.InitiatorId, cancellationToken);

			var perception = character.Skills["Perception"];
			perception.Proficiency = request.Proficiency;

			await characterService.SaveCharacter(character, cancellationToken);
		}
	}
}
