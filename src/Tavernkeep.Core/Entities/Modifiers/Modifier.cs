using Tavernkeep.Core.Contracts.Enums;

namespace Tavernkeep.Core.Entities.Modifiers
{
	public class Modifier
    {
        public ModifierTarget Target { get; set; }
        public ModifierScaling Scaling { get; set; }
        public ModifierType Type { get; set; }
        public int Value { get; set; }
        public bool IsBonus { get; set; }
        public bool IsPenalty => !IsBonus;
    }
}
