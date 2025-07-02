namespace Tavernkeep.Utility.Parser.IO;

internal class RelativeFile
{
	private static readonly string BaseDirectory = AppContext.BaseDirectory;
	public static string ReadAllText(string relativePath) => File.ReadAllText(Path.Combine(BaseDirectory, relativePath));
}
