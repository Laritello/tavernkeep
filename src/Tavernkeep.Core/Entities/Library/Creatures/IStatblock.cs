using Tavernkeep.Core.Entities.Pathfinder.Conditions;

namespace Tavernkeep.Core.Entities.Library.Creatures
{
	public interface IStatblock
	{
		public void ApplyConditions(ICollection<CreatureConditionRecord> conditions);
		public bool IsDividerEnabled { get; set; }
		public string HTML { get; }
	}
}
