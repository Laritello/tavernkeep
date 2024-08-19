using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tavernkeep.Core.Entities.Pathfinder.Classes;

namespace Tavernkeep.Core.Entities.Pathfinder.Builds
{
	public class Class
	{
		public string Name { get; set; } = default!;
		public List<LevelProgression> Progression { get; set; }

		public Class()
		{
			Progression = [];
		}

		public LevelProgression this[int level] => Progression.FirstOrDefault(p => p.Level == level) ?? new(level);

		public static Class Empty => new();
	}
}
