using System.ComponentModel.DataAnnotations.Schema;

namespace Tavernkeep.Core.Entities.Pathfinder.Properties
{
	[Table("CharacterClass")]
	public class Class
	{
		#region Constructors

		public Class(string name, int healthPerLevel)
		{
			Name = name;
			HealthPerLevel = healthPerLevel;
		}

		#endregion

		#region Properties
		public Guid Id { get; set; }
		public required Character Owner { get; set; }

		public string Name { get; set; }
		public int HealthPerLevel { get; set; }

		#endregion
	}
}
