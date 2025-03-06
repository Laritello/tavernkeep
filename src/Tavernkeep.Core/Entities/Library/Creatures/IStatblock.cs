using Tavernkeep.Core.Entities.Pathfinder.Conditions;

namespace Tavernkeep.Core.Entities.Library.Creatures
{
	public interface IStatblock
	{
		public bool IsDividerEnabled { get; set; }
		public string HTML { get; }
	}
}
