﻿using System.Text.Json.Serialization;
using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Contracts.Interfaces;
using Tavernkeep.Core.Evaluators.Properties;

namespace Tavernkeep.Core.Entities.Pathfinder.Properties
{
	public class Ability
	{
		#region Backing fields

		private IValueEvaluator<int>? _scoreEvaluator;

		#endregion

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
		public int Score
		{
			get
			{
				_scoreEvaluator ??= new AbilityScorePropertyEvaluator(this);
				return _scoreEvaluator.Value;
			}
		}
		public int Modifier => (Score - 10) / 2;

		#endregion
	}
}
