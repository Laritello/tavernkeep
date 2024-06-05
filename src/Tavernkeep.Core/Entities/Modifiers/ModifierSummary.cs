namespace Tavernkeep.Core.Entities.Modifiers
{
    public class ModifierSummary
    {
        public List<Modifier> Modifiers { get; set; }
        public Modifier? ActiveBonus { get; set; }
        public Modifier? ActivePenalty { get; set; }
        public int Total { get; set; }

        public ModifierSummary(List<Modifier> modifiers, int total, Modifier? activeBonus, Modifier? activePenalty)
        {
            Modifiers = modifiers;
            Total = total;
            ActiveBonus = activeBonus;
            ActivePenalty = activePenalty;
        }
    }
}
