using MediatR;
using Tavernkeep.Core.Contracts.Enums;

namespace Tavernkeep.Application.UseCases.Characters.Commands.EditSavingThrow
{
	public class EditSavingThrowsCommand(Guid initiatorId, Guid characterId, Dictionary<SavingThrowType, Proficiency> proficiencies) : IRequest
	{
		public Guid InitiatorId { get; set; } = initiatorId;
		public Guid CharacterId { get; set; } = characterId;
		public Dictionary<SavingThrowType, Proficiency> Proficiencies { get; set; } = proficiencies;
	}
}
