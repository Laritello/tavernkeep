using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using Tavernkeep.Core.Interfaces;

namespace Tavernkeep.Core.Entities.Pathfinder.Properties
{
	[Table("CharacterAbility")]
	[PrimaryKey(nameof(Id))]
	public class Ability : INamedProperty
	{
		#region Constructors

		public Ability(string name, int score)
		{
			Name = name;
			Score = score;
		}

		#endregion

		#region Properties
		public Guid Id { get; set; }
		public required Character Owner { get; set; }

		public string Name { get; set; }

		public int Score { get; set; }
		public int Modifier => (Score - 10) / 2;

		#endregion
	}
}
