using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Contracts.Interfaces;
using Tavernkeep.Core.Entities.Snapshots;
using Tavernkeep.Core.Evaluators.Properties;
using Tavernkeep.Core.Interfaces;

namespace Tavernkeep.Core.Entities.Pathfinder.Properties
{
	[Table("CharacterSavingThrow")]
	public class SavingThrow : INamedProperty
	{
		#region Backing fields

		private IValueEvaluator<int>? _savingThrowBonusEvaluator;

		#endregion

		#region Constructors

		public SavingThrow(string name, Proficiency proficiency)
		{
			Name = name;
			Proficiency = proficiency;
		}

		#endregion

		#region Properties

		[JsonIgnore]
		[Key, Column(Order = 0)]
		public required Character Owner { get; set; }

		[Key, Column(Order = 1)]
		public string Name { get; set; }

		public required Ability Ability { get; set; }

		public Proficiency Proficiency { get; set; }

		public int Bonus
		{
			get
			{
				_savingThrowBonusEvaluator ??= new SavingThrowBonusPropertyEvaluator(this);
				return _savingThrowBonusEvaluator.Value;
			}
		}

		#endregion

		#region Methods

		public SavingThrowSnapshot AsSnapshot() => new(Name, Proficiency, Bonus);

		#endregion
	}
}
