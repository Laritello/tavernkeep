namespace Tavernkeep.Core.Extensions
{
	public static class StringExtensions
	{
		public static string HtmlWrapIn(this string text, string container) => $"<{container}>{text}</{container}>";
	}
}
