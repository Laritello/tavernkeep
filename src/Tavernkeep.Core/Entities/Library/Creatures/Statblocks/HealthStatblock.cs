using Scriban;
using System.Text.Json.Serialization;

namespace Tavernkeep.Core.Entities.Library.Creatures.Statblocks
{
	public sealed class HealthStatblock(List<StatblockSection> sections) : IStatblock
	{
		private readonly string html_template = @"<p class='hang'>{{ for section in sections }}<strong>{{ section.header }}</strong>{{ for span in section.spans }} {{ span.html }} {{ end }} {{ end }}</p>";
		public List<StatblockSection> Sections { get; set; } = sections;
		public bool IsDividerEnabled { get; set; }

		[JsonIgnore]
		public string HTML => GenerateHTML();

		private string GenerateHTML()
		{
			//var template = Template.Parse(@"
			//	<p class='hang'>
			//		{{ for section in sections }}
			//			<strong>{{ section.header }}</strong>
			//			{{ for span in section.spans }}
			//				{{ span.html }}
			//			{{ end }}
			//		{{ end }}
			//	</p>
			//	");

			var template = Template.Parse(html_template);
			var html = template.Render(new { Sections });

			if (IsDividerEnabled)
			{
				html = html + Environment.NewLine + "<div class='divider'></div>";
			}

			return html;
		}
	}
}
