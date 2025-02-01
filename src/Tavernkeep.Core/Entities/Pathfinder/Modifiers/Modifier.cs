using System.Text.Json.Serialization;
using Tavernkeep.Core.Contracts.Enums;

namespace Tavernkeep.Core.Entities.Pathfinder.Modifiers
{
	public class Modifier
	{
		public List<string> Targets { get; set; } = [];
		[JsonConverter(typeof(JsonStringEnumConverter))]
		public ModifierScaling Scaling { get; set; }
		[JsonConverter(typeof(JsonStringEnumConverter))]
		public ModifierType Type { get; set; }
		public int Value { get; set; }
		public bool IsBonus { get; set; }
		public bool IsPenalty => !IsBonus;

		public Modifier Copy()
		{
			return new Modifier
			{
				Targets = [.. Targets],
				Scaling = Scaling,
				Type = Type,
				Value = Value,
				IsBonus = IsBonus
			};
		}
	}
}
