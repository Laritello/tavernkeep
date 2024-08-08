namespace Tavernkeep.Core.Entities.Pathfinder
{
	public class Health
	{
		#region Constructors

		public Health() { }

		#endregion

		public int Current { get; set; }
		public int Max { get; set; }
		public int Temporary { get; set; }
	}
}
