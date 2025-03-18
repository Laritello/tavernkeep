using System.Text.Json.Serialization;

namespace Tavernkeep.Statblock.Parser.Entities
{
	internal class PathbuilderRecord
	{
		[JsonPropertyName("name")]
		public string Name { get; set; }

		[JsonPropertyName("size")]
		public string Size { get; set; }

		[JsonPropertyName("alignment")]
		public string Alignment { get; set; }

		[JsonPropertyName("traits")]
		public string Traits { get; set; }

		[JsonPropertyName("level")]
		public string Level { get; set; }

		[JsonPropertyName("statblock")]
		public string Statblock { get; set; }
	}
}
