using System.Text.Json.Serialization;

namespace Tavernkeep.Core.Entities.Pathfinder.Properties
{
	public class Health
	{
		#region Backing fields

		private int _current;
		private int _temporary;

		#endregion

		#region Constructors
		public Health()
		{

		}

		public Health(Character owner)
		{
			Owner = owner;
		}

		#endregion

		[JsonIgnore]
		public Character Owner { get; set; } = default!;

		public int Current
		{
			get => _current;
			set => _current = Math.Clamp(value, 0, Max);
		}

		public int Max { get; set; }

		public int Temporary
		{
			get => _temporary;
			set => _temporary = Math.Max(0, value);
		}

		public void Heal(int heal)
		{
			Current = Math.Clamp(Current + heal, 0, Max);
		}

		public void Damage(int damage)
		{
			var leftOverChange = damage;

			if (Temporary > 0)
			{
				leftOverChange = Math.Max(0, damage - Temporary);
				Temporary = Math.Max(0, Temporary - damage);
			}

			if (leftOverChange > 0)
			{
				Current = Math.Clamp(Current - leftOverChange, 0, Max);
			}
		}
	}
}
