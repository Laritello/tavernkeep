using Tavernkeep.Core.Entities.Pathfinder.Builds.Snapshots;

namespace Tavernkeep.Core.Entities.Pathfinder.Builds.Conversion
{
	public interface IBuildConverter<TEntity, TTemplate>
	{
		public TEntity Convert();
		public TTemplate Restore();
		public BuildSnapshot Snapshot();
	}
}
