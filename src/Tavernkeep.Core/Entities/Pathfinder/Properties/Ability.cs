using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Tavernkeep.Core.Interfaces;

namespace Tavernkeep.Core.Entities.Pathfinder.Properties
{
	[Table("CharacterAbility")]
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

		[Key, Column(Order = 0)]
		public required Character Owner { get; set; }

		[Key, Column(Order = 1)]
		public string Name { get; set; }
		public int Score { get; set; }
		public int Modifier => (Score - 10) / 2;

		#endregion
	}
}
