using System.Text.Json.Serialization;
using Tavernkeep.Core.Evaluators.Properties;
using Tavernkeep.Core.Contracts.Interfaces;

namespace Tavernkeep.Core.Entities.Pathfinder.Properties
{
	public class ArmorClass
	{
		#region Backing fields

		private IPropertyEvaluator<int>? _armorClassEvaluator;

		#endregion

		#region Constructors

		public ArmorClass()
		{

		}

		public ArmorClass(Character character)
		{
			Owner = character;
			Proficiencies = new();
		}

		#endregion

		#region Properties

		[JsonIgnore]
		public Character Owner { get; set; } = default!;
		public ArmorProficiencies Proficiencies { get; set; }
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
