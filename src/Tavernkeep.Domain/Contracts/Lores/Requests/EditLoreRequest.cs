using Tavernkeep.Domain.Contracts.Enums;

namespace Tavernkeep.Domain.Contracts.Lores.Requests
{
	public class EditLoreRequest
	{
		public Guid CharacterId { get; set; }
		public string Topic { get; set; } = default!;
		public Proficiency Proficiency { get; set; } = default!;
	}
}
