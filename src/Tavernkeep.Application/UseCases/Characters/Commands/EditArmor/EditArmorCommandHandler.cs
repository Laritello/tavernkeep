using MediatR;
using Tavernkeep.Application.Interfaces;

namespace Tavernkeep.Application.UseCases.Characters.Commands.EditArmor
{
	public class EditArmorCommandHandler(ICharacterService characterService) : IRequestHandler<EditArmorCommand>
	{
		public async Task Handle(EditArmorCommand request, CancellationToken cancellationToken)
		{
			var character = await characterService.RetrieveCharacterForAction(request.CharacterId, request.InitiatorId, cancellationToken);

			character.Armor.Equipped.Type = request.Type;
			character.Armor.Equipped.Bonus = request.Bonus;
			character.Armor.Equipped.HasDexterityCap = request.HasDexterityCap;
			character.Armor.Equipped.DexterityCap = request.DexterityCap;

			foreach (var key in request.Proficiencies.Keys)
			{
				character.Armor.Proficiencies[key] = request.Proficiencies[key];
			}

			await characterService.SaveCharacter(character, cancellationToken);
		}
	}
}
