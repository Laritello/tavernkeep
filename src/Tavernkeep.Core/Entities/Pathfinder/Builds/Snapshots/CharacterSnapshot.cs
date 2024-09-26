namespace Tavernkeep.Core.Entities.Pathfinder.Builds.Snapshots
{
	public class CharacterSnapshot
	{
		public int Level { get; set; }
		public BuildSnapshot General { get; set; }
		public BuildSnapshot Ancestry { get; set; }
		public BuildSnapshot Background { get; set; }
		public BuildSnapshot Class { get; set; }

		public CharacterSnapshot()
		{
			Level = 1;
			General = new BuildSnapshot();
			Ancestry = new BuildSnapshot();
			Background = new BuildSnapshot();
			Class = new BuildSnapshot();
		}
	}
}
