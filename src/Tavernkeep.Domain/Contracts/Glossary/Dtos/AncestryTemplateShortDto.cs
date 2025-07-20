using Tavernkeep.Domain.Contracts.Enums;

namespace Tavernkeep.Domain.Contracts.Glossary.Dtos;

public record AncestryTemplateShortDto
{
	public string Id { get; set; } = default!;
	public string Name { get; set; } = default!;
	public string Description { get; set; } = default!;
	public List<string> Traits { get; set; } = [];
	public Rarity Rarity { get; set; }
}
