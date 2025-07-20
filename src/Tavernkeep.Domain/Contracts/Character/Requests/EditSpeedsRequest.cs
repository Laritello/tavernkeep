using Tavernkeep.Domain.Contracts.Character.Dtos;
using Tavernkeep.Domain.Contracts.Enums;

namespace Tavernkeep.Domain.Contracts.Character.Requests;

public class EditSpeedsRequest
{
	public Dictionary<SpeedType, SpeedEditDto> Speeds { get; set; } = default!;
}
