using Tavernkeep.Core.Entities.Pathfinder.Builds.Advancements;

namespace Tavernkeep.Core.Entities.Pathfinder.Builds
{
	public class LevelProgression
	{
		#region Constructors

		public LevelProgression(int level)
		{
			Level = level;
		}

		#endregion

		#region Properties

		public int Level { get; set; }
		public List<Advancement> Advancements { get; set; } = [];

		#endregion

		#region Operators

		public static LevelProgression operator +(LevelProgression a, LevelProgression b)
		{
			if (a.Level != b.Level)
				throw new InvalidOperationException("To combine two LevelProgression object they must have the same level.");

			return new(a.Level)
			{
				Advancements = [.. a.Advancements, .. b.Advancements]
			};
		}

		#endregion
	}
}
