namespace Tavernkeep.Core.Entities.Modifiers
{
	// TODO: Check if struct is better
	public class ModifierSummary(List<Modifier> modifiers, int total, Modifier? activeBonus, Modifier? activePenalty)
	{
		public List<Modifier> Modifiers { get; set; } = modifiers;
		public Modifier? ActiveBonus { get; set; } = activeBonus;
		public Modifier? ActivePenalty { get; set; } = activePenalty;
		public int Total { get; set; } = total;
	}
}
