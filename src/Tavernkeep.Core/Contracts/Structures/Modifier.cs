using Tavernkeep.Core.Contracts.Enums;

namespace Tavernkeep.Core.Contracts.Structures
{
	public struct Modifier
	{
		public ModifierType Type { get; set; }
		public required string Formula { get; set; }
	}
}
