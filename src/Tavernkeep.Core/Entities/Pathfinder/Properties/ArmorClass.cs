﻿using System.Text.Json.Serialization;
using Tavernkeep.Core.Contracts.Interfaces;
using Tavernkeep.Core.Evaluators.Properties;

namespace Tavernkeep.Core.Entities.Pathfinder.Properties
{
	public class ArmorClass
	{
		#region Backing fields

		private IValueEvaluator<int>? _armorClassEvaluator;

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
