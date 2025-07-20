using Tavernkeep.Domain.Contracts.Structures;
using Tavernkeep.Domain.Evaluators.Properties;

namespace Tavernkeep.Domain.Entities.Pathfinder.Properties;

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
