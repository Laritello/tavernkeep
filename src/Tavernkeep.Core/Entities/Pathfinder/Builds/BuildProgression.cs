namespace Tavernkeep.Core.Entities.Pathfinder.Builds
{
	public class BuildProgression
	{
		#region Backing fields

		private readonly List<LevelProgression> _levels;

		#endregion

		#region Constructors

		public BuildProgression()
		{
			_levels = [];
		}

		#endregion

		#region Properties

		public IReadOnlyCollection<LevelProgression> Levels => _levels.AsReadOnly();

		#endregion

		#region Indexers

		public LevelProgression this[int level] => _levels[level - 1];

		#endregion
	}
}
