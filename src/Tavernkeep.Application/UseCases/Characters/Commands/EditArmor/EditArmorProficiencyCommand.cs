using MediatR;
using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Entities.Pathfinder.Properties;

namespace Tavernkeep.Application.UseCases.Characters.Commands.EditArmor
{
	public class EditArmorProficiencyCommand(Guid initiatorId, Guid characterId, ArmorType type, Proficiency proficiency) : IRequest<ArmorProficiencies>
	{
		public Guid InitiatorId { get; set; } = initiatorId;
		public Guid CharacterId { get; set; } = characterId;
		public ArmorType Type { get; set; } = type;
		public Proficiency Proficiency { get; set; } = proficiency;
	}
}
