using Tavernkeep.Core.Contracts.Enums;

namespace Tavernkeep.Statblock.Parser.Extensions
{
	internal static class ParsingExtensions
	{
		public static ActionAmount ToActionAmount(this string source)
		{
			return source switch
			{
				"[free-action]" => ActionAmount.Free,
				"[reaction]" => ActionAmount.Reaction,
				"[one-action]" => ActionAmount.One,
				"[two-actions]" => ActionAmount.Two,
				"[three-actions]" => ActionAmount.Three,
				_ => throw new NotImplementedException(),
			};
		}
	}
}
