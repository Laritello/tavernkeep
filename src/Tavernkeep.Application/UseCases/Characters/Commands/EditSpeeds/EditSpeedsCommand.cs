using MediatR;
using Tavernkeep.Domain.Contracts.Character.Dtos;
using Tavernkeep.Domain.Contracts.Enums;

namespace Tavernkeep.Application.UseCases.Characters.Commands.EditSpeeds
{
	public class EditSpeedsCommand(Guid initiatorId, Guid characterId, Dictionary<SpeedType, SpeedEditDto> speeds) : IRequest
	{
		public Guid InitiatorId { get; set; } = initiatorId;
		public Guid CharacterId { get; set; } = characterId;
		public Dictionary<SpeedType, SpeedEditDto> Speeds { get; set; } = speeds;
	}
}
