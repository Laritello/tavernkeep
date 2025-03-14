using System.Text;
using Tavernkeep.Core.Entities.Pathfinder.Conditions;
using Tavernkeep.Core.Statblocks.Abstractions;

namespace Tavernkeep.Core.Entities.Library.Creatures
{
	public sealed class CreatureInstance
	{
		private CreatureInstance() { }

		public required string Name { get; set; }
		public required string Type { get; set; }
		public int Level { get; set; }
		public int Health { get; set; }
		public ICollection<string> Traits { get; set; } = [];
		public ICollection<IStatblockComponent> Blocks { get; set; } = [];

		public int Perception => 0;
		public int ArmorClass => 0;

		public Dictionary<string, int> SavingThrows = [];

		public int GetSkillBonus(string skillName) => 0;

		public string GetStatBlock()
		{
			StringBuilder sb = new();

			foreach (var block in Blocks)
			{
				sb.AppendLine(block.HTML);
			}

			var html = sb.ToString();

			return $"<div class='flex flex-col p-2 text-pf w-full'>{html}</div>";
		}

		public static CreatureInstance Create(Creature creature, ICollection<ConditionRecord> records)
		{
			CreatureInstance instance = new()
			{
				Name = creature.Name,
				Type = creature.Type,
				Level = creature.Level,
				Health = creature.Health,
				Traits = creature.Traits,
				Blocks = creature.Blocks.Select(x => x.Copy()).ToList(),
			};

			foreach (var block in instance.Blocks)
			{
				block.ApplyConditions(records);
			}

			return instance;
		}
	}
}
