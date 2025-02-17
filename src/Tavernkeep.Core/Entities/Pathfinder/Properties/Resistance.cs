namespace Tavernkeep.Core.Entities.Pathfinder.Properties
{
	public class Resistance
	{
		public required string Type { get; set; }
		public int Value { get; set; }
		public List<string> Exceptions { get; set; } = [];
		public List<string> Doubled { get; set; } = [];
	}
}
