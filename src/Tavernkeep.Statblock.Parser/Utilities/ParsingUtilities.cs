using Tavernkeep.Core.Statblocks.Abstractions.Tokens;

namespace Tavernkeep.Statblock.Parser.Utilities
{
	public static class ParsingUtilities
	{
		public static List<IToken> Collect(List<IToken> tokens, ref int i, int length, Func<string, bool>? continueRule = null)
		{
			// Assumed that if we executing collect method the first token is required.
			int start = i++;
			bool sectionEnd = false;

			Stack<IToken> listTokens = [];

			while (i < length)
			{
				switch (tokens[i])
				{
					case IKeywordToken keyword:
						if (listTokens.Count == 0 && (continueRule is null || !continueRule.Invoke(keyword.Name)))
						{
							sectionEnd = true;
						}
						break;
					case IListToken list:
						if (list.IsStart)
						{
							listTokens.Push(list);
						}
						else if (listTokens.Peek() is IListToken top && top.IsStart)
						{
							listTokens.Pop();
						}
						else
						{
							throw new Exception($"Wrong order of {nameof(IListToken)}");
						}
						break;
					case IListItemToken listItem:
						if (listItem.IsStart)
						{
							listTokens.Push(listItem);
						}
						else if (listTokens.Peek() is IListItemToken top && top.IsStart)
						{
							listTokens.Pop();
						}
						else
						{
							throw new Exception($"Wrong order of {nameof(IListItemToken)}");
						}
						break;
				}

				if (sectionEnd)
				{
					break;
				}

				i++;
			}

			return tokens[start..i];
		}
	}
}
