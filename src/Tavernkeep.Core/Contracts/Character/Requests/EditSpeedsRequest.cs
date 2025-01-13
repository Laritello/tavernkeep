using Tavernkeep.Core.Contracts.Character.Dtos;
using Tavernkeep.Core.Contracts.Enums;

namespace Tavernkeep.Core.Contracts.Character.Requests
{
	public class EditSpeedsRequest
	{
		public Dictionary<SpeedType, SpeedEditDto> Speeds { get; set; } = default!;
	}
}
