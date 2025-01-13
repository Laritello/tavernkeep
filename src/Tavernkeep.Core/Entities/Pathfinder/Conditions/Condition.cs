using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Entities.Pathfinder.Modifiers;

namespace Tavernkeep.Core.Entities.Pathfinder.Conditions
{
	public class Condition
	{
		#region Constructors

		public Condition() { }

		#endregion

		#region Properties

		public string Name { get; set; } = default!;
		public bool HasLevels { get; set; }
		public int Level { get; set; }

		public List<Condition> Related { get; set; } = [];
		public List<Modifier> Modifiers { get; set; } = [];

		#endregion

		#region Methods

		public IEnumerable<Modifier> CollectModifiers(Character character)
		{
			var relatedModifiers = Related.SelectMany(r => r.Modifiers);
			var combinedModifiers = relatedModifiers.Concat(Modifiers);

			foreach (var modifier in combinedModifiers)
			{
				switch (modifier.Scaling)
				{
					case ModifierScaling.ConditionLeveled:
						modifier.Value = Level;
						break;
					case ModifierScaling.CharacterLeveled:
						modifier.Value = Level * character.Level;
						break;
				}
			}

			return combinedModifiers;
		}

		#endregion
	}
}
