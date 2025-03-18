using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Tavernkeep.Core.Entities.Base;
using Tavernkeep.Core.Statblocks.Abstractions;
using Tavernkeep.Core.Statblocks.Components;

namespace Tavernkeep.Core.Entities.Library.Creatures
{
	[Table("Creature")]
	public class Creature : GuidEntity
	{
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

		public void AddBlock(IStatblockComponent block)
		{
			if (Blocks.Count > 0 && block is TraitsStatblockComponent or SpeedStatblockComponent or DefenseStatblockComponent)
			{
				Blocks.Last().IsDividerEnabled = true;
			}

			Blocks.Add(block);
		}

		public T GetBlock<T>() where T : IStatblockComponent
		{
			return (T)Blocks.First(x => x is T);
		}

		public Creature Copy()
		{
			Creature instance = new()
			{
				Name = Name,
				Type = Type,
				Level = Level,
				Health = Health,
				Traits = Traits,
				Blocks = Blocks.Select(x => x.Copy()).ToList(),
			};

			return instance;
		}
	}
}
