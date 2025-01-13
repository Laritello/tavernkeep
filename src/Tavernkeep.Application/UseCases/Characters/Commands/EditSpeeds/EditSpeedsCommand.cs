using MediatR;
using Tavernkeep.Core.Contracts.Character.Dtos;
using Tavernkeep.Core.Contracts.Enums;

namespace Tavernkeep.Application.UseCases.Characters.Commands.EditSpeeds
{
	public class EditSpeedsCommand(Guid initiatorId, Guid characterId, Dictionary<SpeedType, EditSpeedDto> speeds) : IRequest
	{
		public Guid InitiatorId { get; set; } = initiatorId;
		public Guid CharacterId { get; set; } = characterId;
		public Dictionary<SpeedType, EditSpeedDto> Speeds { get; set; } = speeds;
	}
}
