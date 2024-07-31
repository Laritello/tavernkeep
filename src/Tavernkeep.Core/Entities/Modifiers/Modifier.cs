using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Contracts.Interfaces;

namespace Tavernkeep.Core.Entities.Modifiers
{
	public class Modifier(IModifierSource source)
    {
        public IModifierSource Source { get; set; } = source;
        public ModifierTarget Target { get; set; }
        public int Value { get; set; }
        public bool IsBonus => Value > 0;
        public bool IsPenalty => Value < 0;
    }
}
