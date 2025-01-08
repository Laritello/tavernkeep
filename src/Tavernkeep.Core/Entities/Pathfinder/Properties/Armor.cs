using System.Text.Json.Serialization;
using Tavernkeep.Core.Contracts.Structures;
using Tavernkeep.Core.Evaluators.Properties;

namespace Tavernkeep.Core.Entities.Pathfinder.Properties
{
	public class Armor
	{
		#region Backing fields

		private ArmorClassPropertyEvaluator? _armorClassEvaluator;

		#endregion

		#region Constructors

		public Armor()
		{

		}

		public Armor(Character character)
		{
			Owner = character;
			Proficiencies = new();
			Equipped = new();
		}

		#endregion

		#region Properties

		[JsonIgnore]
		public Character Owner { get; set; } = default!;
		public ArmorProficiencies Proficiencies { get; set; }
		public EquippedArmor Equipped { get; set; }
		public int Class
		{
			get
			{
				_armorClassEvaluator ??= new ArmorClassPropertyEvaluator(this);
				return _armorClassEvaluator.Value;
			}
		}

		#endregion
	}
}
