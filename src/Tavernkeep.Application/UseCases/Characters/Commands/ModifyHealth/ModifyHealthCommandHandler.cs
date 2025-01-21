using MediatR;
using Tavernkeep.Application.Interfaces;

namespace Tavernkeep.Application.UseCases.Characters.Commands.ModifyHealth
{
	public class ModifyHealthCommandHandler(ICharacterService characterService) : IRequestHandler<ModifyHealthCommand>
	{
		public async Task Handle(ModifyHealthCommand request, CancellationToken cancellationToken)
		{
			var character = await characterService.RetrieveCharacterForEdit(request.CharacterId, request.InitiatorId, cancellationToken);

			if (request.Change > 0)
				character.Health.Heal(request.Change);
			else
				character.Health.Damage(Math.Abs(request.Change));

			await characterService.SaveCharacter(character, cancellationToken);
		}
	}
}
