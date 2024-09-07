using Tavernkeep.Core.Contracts.Enums;

namespace Tavernkeep.Core.Contracts.Glossary.Dtos
{
	public record ClassTemplateShortDto
	{
		public string Id { get; set; } = default!;
		public string Name { get; set; } = default!;
		public string Description { get; set; } = default!;
		public List<string> Traits { get; set; } = [];
		public Rarity Rarity { get; set; }
	}
}
