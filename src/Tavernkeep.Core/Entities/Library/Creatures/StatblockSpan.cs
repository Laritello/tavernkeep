namespace Tavernkeep.Core.Entities.Library.Creatures
{
	public struct StatblockSpan
	{
		public string Text { get; set; }
		public bool IsBold { get; set; }
		public bool IsItalic { get; set; }
		public readonly string HTML
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

		private static string WrapIn(string text, string container) => $"<{container}>{text}</{container}>";
		private static string ProcessImages(string text)
		{
			return text.Replace("[free]", "<img src='./resources/pf-action-free.png' class='text-img'></img>")
				.Replace("[reaction]", "<img src='./resources/pf-action-reaction.png' class='text-img'></img>")
				.Replace("[one-action]", "<img src='./resources/pf-action-1.png' class='text-img'></img>")
				.Replace("[two-actions]", "<img src='./resources/pf-action-2.png' class='text-img'></img>")
				.Replace("[three-actions]", "<img src='./resources/pf-action-3.png' class='text-img'></img>");
		}
	}
}
