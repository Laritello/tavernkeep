using NCalc;
using System.ComponentModel.DataAnnotations.Schema;
using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Contracts.Structures;
using Tavernkeep.Core.Entities.Base;

namespace Tavernkeep.Core.Entities.Pathfinder.Conditions
{
	[Table("ConditionRecord")]
	public abstract class ConditionRecord : GuidEntity
	{
		#region Constructors

		public ConditionRecord() { }

		#endregion

		#region Properties

		public required Condition Condition { get; set; }
		public int? Level { get; set; }

		#endregion

		#region Methods

		public abstract int this[ICollection<string> targetNames] { get; }

		public bool HasModifier(ModifierType type, params ICollection<string> targetNames)
		{
			foreach(var targetName in targetNames)
			{
				if (Condition.Modifiers.TryGetValue(targetName, out Modifier value) && value.Type == type)
				{
					return true;
				}
			}

			return false;
		}
			

		#endregion
	}
}
