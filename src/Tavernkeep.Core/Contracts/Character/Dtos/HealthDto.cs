namespace Tavernkeep.Core.Contracts.Character.Dtos
{
	public class HealthDto
	{
		public required int Max { get; set; }
		public required int Current { get; set; }
		public required int Temporary { get; set; }
	}
}
