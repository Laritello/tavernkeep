namespace Tavernkeep.Core.Entities.Pathfinder
{
	public class Health
	{
		#region Backing fields

		private int _current;
		private int _max;
		private int _temporary;

		#endregion

		#region Constructors

		public Health()
		{
			Max = 1;
			Current = 1;
		}

		public Health(int max, int current, int temporary) 
		{
			Max = max;
			Current = current;
			Temporary = temporary;
		}

		#endregion

		public int Current
		{
			get => _current;
			set => _current = Math.Clamp(value, 0, Max);
		}

		public int Max
		{
			get => _max;
			set => _max = Math.Max(0, value);
		}

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
