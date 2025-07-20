using System.Text.Json.Serialization;

namespace Tavernkeep.Utility.Parser.Json;

public class PathbuilderRecord
{
	[JsonPropertyName("name")]
	public required string Name { get; set; }

	[JsonPropertyName("size")]
	public required string Size { get; set; }

	[JsonPropertyName("alignment")]
	public required string Alignment { get; set; }

	[JsonPropertyName("traits")]
	public required string Traits { get; set; }

	[JsonPropertyName("level")]
	public required string Level { get; set; }

	[JsonPropertyName("statblock")]
	public required string Statblock { get; set; }
}
