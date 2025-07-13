namespace Tavernkeep.Core.Extensions
{
	public static class StringExtensions
	{
		public static string HtmlWrapIn(this string text, string container) => $"<{container}>{text}</{container}>";

		public static string Capitalize(this string input) =>
			input switch
			{
				null => throw new ArgumentNullException(nameof(input)),
				"" => throw new ArgumentException($"{nameof(input)} cannot be empty", nameof(input)),
				_ => string.Concat(input[0].ToString().ToUpper(), input.AsSpan(1))
			};
	}
}
