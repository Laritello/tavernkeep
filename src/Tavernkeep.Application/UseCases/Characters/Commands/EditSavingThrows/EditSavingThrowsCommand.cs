using MediatR;
using Tavernkeep.Domain.Contracts.Enums;

namespace Tavernkeep.Application.UseCases.Characters.Commands.EditSavingThrows
{
	public class EditSavingThrowsCommand(Guid initiatorId, Guid characterId, Dictionary<string, Proficiency> proficiencies) : IRequest
	{
		public Guid InitiatorId { get; set; } = initiatorId;
		public Guid CharacterId { get; set; } = characterId;
		public Dictionary<string, Proficiency> Proficiencies { get; set; } = proficiencies;
	}
}
