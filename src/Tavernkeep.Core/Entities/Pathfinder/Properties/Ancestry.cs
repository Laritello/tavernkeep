using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tavernkeep.Core.Entities.Pathfinder.Properties
{
	[Table("CharacterAncestry")]
	public class Ancestry
	{
		#region Constructors

		public Ancestry(string name, int health)
		{
			Name = name;
			Health = health;
		}

		#endregion

		#region Properties

		[Key, Column(Order = 0)]
		public required Character Owner { get; set; }

		[Key, Column(Order = 1)]
		public string Name { get; set; }
		public int Health { get; set; }

		#endregion
	}
}
