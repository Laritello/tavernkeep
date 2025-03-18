using System.Windows.Documents;
using System.Windows;
using Tavernkeep.Core.Statblocks.Abstractions.Tokens;
using Tavernkeep.Core.Statblocks.Tokens;
using Tavernkeep.Statblock.Parser.Parsing.Abstractions;

namespace Tavernkeep.Statblock.Parser.Parsing.Tokenizers
{
	internal sealed class FlowDocumentTokenizer : ITokenizer<FlowDocument>
	{
		public List<IToken> Tokenize(FlowDocument document)
		{
			List<Run> runs = [];

			foreach (Block block in document.Blocks)
			{
				HandleBlock(block, runs);
			}

			return runs.Select(x => new TextToken(x.Text, x.FontWeight == FontWeights.Bold, x.FontStyle == FontStyles.Italic) as IToken).ToList();
		}

		private void HandleBlock(Block block, ICollection<Run> runs)
		{
			if (block is Paragraph paragraph)
			{
				HandleParagraph(paragraph, runs);
			}
		}

		private void HandleParagraph(Paragraph paragraph, ICollection<Run> runs)
		{
			foreach (Inline inline in paragraph.Inlines)
			{
				HandleInline(inline, runs);
			}
		}

		private void HandleInline(Inline inline, ICollection<Run> runs)
		{
			switch (inline)
			{
				case Run run:
					//HandleRun(run);
					runs.Add(run);
					break;

				case Span span:
					HandleSpan(span, runs);
					break;
			}
		}

		private void HandleSpan(Span span, ICollection<Run> runs)
		{
			foreach (Inline sibiling in span.Inlines)
			{
				HandleInline(sibiling, runs);
			}
		}
	}
}
