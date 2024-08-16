using Tavernkeep.Core.Entities.Pathfinder.Builds.Advancements;

namespace Tavernkeep.Core.Entities.Pathfinder.Builds
{
	public class LevelProgression
	{
		#region Constructors

		public LevelProgression(int level)
		{
			Level = level;
			Advancements = [];
		}

		#endregion

		#region Properties

		public int Level { get; set; }
		public List<Advancement> Advancements { get; set; }

		#endregion
	}
}
