using Scriban;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tavernkeep.Core.Entities.Library.Creatures.Statblocks
{
	public class AttritubesStatblock(List<AttributeRecord> attributes) : IStatblock
	{
		public List<AttributeRecord> Attributes { get; set; } = attributes;
		public bool IsDividerEnabled { get; set; }

		public string HTML => GenerateHTML();

		private string GenerateHTML()
		{
			var template = Template.Parse(@"
				<p class='hang'>
					{{ for attribute in attributes }}
						<strong>{{ attribute.name }}</strong>
						{{ attribute.bonus }}{{ if !for.last }},{{ end }}
					{{ end }}
				</p>
				");

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
