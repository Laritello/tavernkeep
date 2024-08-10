using MediatR;
using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Entities.Pathfinder;

namespace Tavernkeep.Application.UseCases.Characters.Commands.EditSavingThrow
{
	public class EditSavingThrowCommand(Guid initiatorId, Guid characterId, SavingThrowType type, Proficiency proficiency) : IRequest<SavingThrow>
	{
		public Guid InitiatorId { get; set; } = initiatorId;
		public Guid CharacterId { get; set; } = characterId;
		public SavingThrowType Type { get; set; } = type;
		public Proficiency Proficiency { get; set; } = proficiency;
	}
}
