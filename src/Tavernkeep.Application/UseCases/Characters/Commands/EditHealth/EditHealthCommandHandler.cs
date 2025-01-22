using MediatR;
using Tavernkeep.Application.Interfaces;
using Tavernkeep.Core.Exceptions;

namespace Tavernkeep.Application.UseCases.Characters.Commands.EditHealth
{
	public class EditHealthCommandHandler(ICharacterService characterService) : IRequestHandler<EditHealthCommand>
	{
		public async Task Handle(EditHealthCommand request, CancellationToken cancellationToken)
		{
			if (request.Current < 0)
				throw new BusinessLogicException($"{nameof(request.Current)} can't be below zero.");

			if (request.Temporary < 0)
				throw new BusinessLogicException($"{nameof(request.Temporary)} can't be below zero.");

			var character = await characterService.RetrieveCharacterForEdit(request.CharacterId, request.InitiatorId, cancellationToken);

			character.Health.Current = request.Current;
			character.Health.Temporary = request.Temporary;

			await characterService.SaveCharacter(character, cancellationToken);
		}
	}
}
