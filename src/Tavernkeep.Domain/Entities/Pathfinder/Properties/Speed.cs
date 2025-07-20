using System.Text.Json.Serialization;
using Tavernkeep.Domain.Contracts.Enums;
using Tavernkeep.Domain.Evaluators.Properties;

namespace Tavernkeep.Domain.Entities.Pathfinder.Properties;

public class Speed
{
	#region Backing fields

	private SpeedPropertyEvaluator? _speedEvaluator;

	#endregion

	#region Constructors

	public Speed()
	{

	}

	public Speed(Character owner, SpeedType type, bool active = false)
	{
		Owner = owner;
		Type = type;
		Active = active;
	}

	#endregion

	#region Properties

	[JsonIgnore]
	public Character Owner { get; set; } = default!;
	public bool Active { get; set; }
	public int Base { get; set; }
	public SpeedType Type { get; set; }
	public int Value
	{
		get
		{
			_speedEvaluator ??= new(this);
			return _speedEvaluator.Value;
		}
	}

	#endregion
}
