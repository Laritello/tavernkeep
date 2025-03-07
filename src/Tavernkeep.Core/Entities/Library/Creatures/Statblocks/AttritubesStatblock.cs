using Scriban;
using System.Text.Json.Serialization;

namespace Tavernkeep.Core.Entities.Library.Creatures.Statblocks
{
	public class AttributesStatblock(List<AttributeRecord> attributes) : IStatblock
	{
		private readonly string html_template = @"<p class='hang'>{{ for attribute in attributes }}<strong>{{ attribute.name }}</strong>{{ attribute.bonus }}{{ if !for.last }},{{ end }} {{ end }}</p>";
		public List<AttributeRecord> Attributes { get; set; } = attributes;
		public bool IsDividerEnabled { get; set; }

		[JsonIgnore]
		public string HTML => GenerateHTML();

		private string GenerateHTML()
		{
			//var template = Template.Parse(@"
			//	<p class='hang'>
			//		{{ for attribute in attributes }}
			//			<strong>{{ attribute.name }}</strong>
			//			{{ attribute.bonus }}{{ if !for.last }},{{ end }}
			//		{{ end }}
			//	</p>
			//	");

			var template = Template.Parse(html_template);
			var html = template.Render(new { Attributes });

			if (IsDividerEnabled)
			{
				html = html + Environment.NewLine + "<div class='divider'></div>";
			}

			return html;
		}
	}

	public record AttributeRecord(string Name, string Bonus);
}
