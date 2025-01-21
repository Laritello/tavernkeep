using MediatR;
using Tavernkeep.Application.Interfaces;

namespace Tavernkeep.Application.UseCases.Characters.Commands.DeleteCharacter
{
	public class DeleteCharacterCommandHandler(ICharacterService characterService) : IRequestHandler<DeleteCharacterCommand>
	{
		public async Task Handle(DeleteCharacterCommand request, CancellationToken cancellationToken)
		{
			var character = await characterService.RetrieveCharacterForEdit(request.CharacterId, request.InitiatorId, cancellationToken);
			await characterService.DeleteCharacter(character, cancellationToken);
		}
	}
}
