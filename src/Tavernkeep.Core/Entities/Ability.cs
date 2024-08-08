using System.Text.Json.Serialization;
using Tavernkeep.Core.Contracts.Enums;

namespace Tavernkeep.Core.Entities
{
	public class Ability
	{
		#region Constructors

		public Ability()
		{

		}

		public Ability(Character owner, AbilityType type)
		{
			Owner = owner;
			Type = type;
		}

		#endregion

		#region Properties

		[JsonIgnore]
		public Character Owner { get; set; } = default!;
		public AbilityType Type { get; set; }
		public int Score { get; set; }
		public int Modifier => (Score - 10) / 2;

		#endregion
	}
}
