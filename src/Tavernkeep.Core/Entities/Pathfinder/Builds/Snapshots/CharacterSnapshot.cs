namespace Tavernkeep.Core.Entities.Pathfinder.Builds.Snapshots
{
	public class CharacterSnapshot
	{
		public BuildSnapshot General { get; set; }
		public BuildSnapshot Ancestry { get; set; }
		public BuildSnapshot Class { get; set; }

		public CharacterSnapshot()
		{
			General = new BuildSnapshot();
			Ancestry = new BuildSnapshot();
			Class = new BuildSnapshot();
		}
	}
}
