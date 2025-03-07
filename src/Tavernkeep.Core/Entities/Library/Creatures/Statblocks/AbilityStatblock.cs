using Scriban;
using System.Text.Json.Serialization;

namespace Tavernkeep.Core.Entities.Library.Creatures.Statblocks
{
	public sealed class AbilityStatblock(List<StatblockSection> sections) : IStatblock
	{
		public List<StatblockSection> Sections { get; set; } = sections;
		public bool IsDividerEnabled { get; set; }

		[JsonIgnore]
		public string HTML => GenerateHTML();

		private string GenerateHTML()
		{
			/** Slighlty hackish way to do this template
			 * The idea is that nested parapgraphs will be teleported
			 * below current paragraph (its illegal in HTML to have nested paragraphs)
			 * So we're gonna use modified offest with hang-nested class
			 * to emulate as if they were nested
			 */
			var template = Template.Parse(@"
				<p class='hang'>
					{{ for section in sections }}
						<strong>{{ section.header }}</strong>
						{{ for span in section.spans }}
							{{ span.html }}
						{{ end }}
						{{ for subsection in section.subsections }}
							<p class='hang-nested'>
								<strong>{{ subsection.header }}</strong>
								{{ for span in subsection.spans }}
									{{ span.html }}
								{{ end }}
							</p>
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
