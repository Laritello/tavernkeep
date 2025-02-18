using MediatR;
using Tavernkeep.Application.Interfaces;
using Tavernkeep.Core.Exceptions;

namespace Tavernkeep.Application.UseCases.Characters.Commands.EditHeroPoints
{
	public class EditHeroPointsCommandHandler(ICharacterService characterService) : IRequestHandler<EditHeroPointsCommand>
	{
		public async Task Handle(EditHeroPointsCommand request, CancellationToken cancellationToken)
		{
			if (request.Amount < 0)
				throw new BusinessLogicException("Hero points amount cannot be less than zero.");

			if (request.Amount > 3)
				throw new BusinessLogicException("Hero points amoint cannot be more than three.");

			var character = await characterService.RetrieveCharacterForAction(request.CharacterId, request.InitiatorId, cancellationToken);

			character.HeroPoints = request.Amount;

			await characterService.SaveCharacter(character, cancellationToken);
		}
	}
}
