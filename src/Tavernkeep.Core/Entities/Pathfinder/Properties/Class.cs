using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
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

		[Key, Column(Order = 0)]
		public required Character Owner { get; set; }

		[Key, Column(Order = 1)]
		public string Name { get; set; }
		public int HealthPerLevel { get; set; }

		#endregion
	}
}
