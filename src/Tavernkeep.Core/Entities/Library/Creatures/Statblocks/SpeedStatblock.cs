using Scriban;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tavernkeep.Core.Entities.Library.Creatures.Statblocks
{
	public sealed class SpeedStatblock(List<StatblockSection> sections) : IStatblock
	{
		public List<StatblockSection> Sections { get; set; } = sections;
		public bool IsDividerEnabled { get; set; }

		public string HTML => GenerateHTML();

		private string GenerateHTML()
		{
			var template = Template.Parse(@"
				<p class='hang'>
					{{ for section in sections }}
						<strong>{{ section.header }}</strong>
						{{ for span in section.spans }}
							{{ span.html }}
						{{ end }}
					{{ end }}
				</p>
				");

			var html = template.Render(new { Sections });

			if (IsDividerEnabled)
			{
				html = html + Environment.NewLine + "<div class='divider'></div>";
			}

			return html;
		}
	}
}
