using Tavernkeep.Core.Contracts.Enums;

namespace Tavernkeep.Core.Contracts.Lores.Requests
{
	public class CreateLoreRequest
	{
		public Guid CharacterId { get; set; }
		public string Topic { get; set; } = default!;
		public Proficiency Proficiency { get; set; } = default!;
	}
}
