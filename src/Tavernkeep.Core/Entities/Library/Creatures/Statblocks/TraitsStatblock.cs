using Scriban;

namespace Tavernkeep.Core.Entities.Library.Creatures.Statblocks
{
	public sealed class TraitsStatblock(ICollection<string> traits) : IStatblock
	{
		public ICollection<string> Traits { get; set; } = traits;

		public bool IsDividerEnabled { get; set; }
		public string HTML => GenerateHTML();

		private string GenerateHTML()
		{
			var template = Template.Parse(@"
			<div class='flex flex-row pt-1'>
				{{ for trait in traits }}
				<div class='pf-trait {{ if trait.size }} pf-trait-size {{ end }}'>{{ trait.name }}</div>
				{{ end }}
			</div>
			");

			var html = template.Render(new { Traits = Traits.Select(x => new { Name = x, Size = IsSize(x) }) });

			if (IsDividerEnabled)
			{
				html = html + Environment.NewLine + "<div class='divider'></div>";
			}

			return html;
		}

		private static string[] sizes = ["tiny", "small", "medium", "large", "huge", "gargantuan"];

		private static bool IsSize(string trait)
		{
			return sizes.Contains(trait.ToLower());
		}
	}
}
