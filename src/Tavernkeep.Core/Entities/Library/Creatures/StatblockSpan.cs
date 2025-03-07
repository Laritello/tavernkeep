using System.Text.Json.Serialization;

namespace Tavernkeep.Core.Entities.Library.Creatures
{
	public class StatblockSpan
	{
		public required string Text { get; set; }
		public required bool IsBold { get; set; }
		public required bool IsItalic { get; set; }

		[JsonIgnore]
		public string HTML
		{
			get
			{
				string html = ProcessImages(Text);

				if (IsBold)
				{
					html = WrapIn(html, "strong");
				}

				if (IsItalic)
				{
					html = WrapIn(html, "em");
				}

				return html;
			}
		}

		public bool SameStyle(bool isBold, bool isItalic) => isBold == IsBold && isItalic == IsItalic;

		private static string WrapIn(string text, string container) => $"<{container}>{text}</{container}>";
		private static string ProcessImages(string text)
		{
			return text.Replace("[free-action]", "<img src='/assets/images/pf-action-free.png' class='text-img'></img>")
				.Replace("[reaction]", "<img src='/assets/images/pf-action-reaction.png' class='text-img'></img>")
				.Replace("[one-action]", "<img src='/assets/images/pf-action-1.png' class='text-img'></img>")
				.Replace("[two-actions]", "<img src='/assets/images/pf-action-2.png' class='text-img'></img>")
				.Replace("[three-actions]", "<img src='/assets/images/pf-action-3.png' class='text-img'></img>");
		}
	}
}
