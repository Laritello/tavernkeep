using Scriban;
using System.Text.Json.Serialization;

namespace Tavernkeep.Core.Entities.Library.Creatures.Statblocks
{
	public sealed class GeneralStatblock(string name, string type, int level) : IStatblock
	{
		public string Name { get; set; } = name;
		public string Type { get; set; } = type;
		public int Level { get; set; } = level;
		public bool IsDividerEnabled { get; set; }

		[JsonIgnore]
		public string HTML => GenerateHTML();

		private string GenerateHTML()
		{
			var template = Template.Parse(@"
			<div class='flex flex-row'>
				<h1 class='flex-1'>{{ name }}</h1>
				<h1>{{ type }} {{ level }}</h1>
			</div>
			");

			var html = template.Render(new { Name, Level, Type });

			if (IsDividerEnabled)
			{
				html = html + Environment.NewLine + "<div class='divider'></div>";
			}

			return html;
		}
	}
}
