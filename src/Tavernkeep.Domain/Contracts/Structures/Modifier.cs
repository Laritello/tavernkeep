using Tavernkeep.Domain.Contracts.Enums;

namespace Tavernkeep.Domain.Contracts.Structures;

public struct Modifier
{
	public ModifierType Type { get; set; }
	public required string Formula { get; set; }
}
