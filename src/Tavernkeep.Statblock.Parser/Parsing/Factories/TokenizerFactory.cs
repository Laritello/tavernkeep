using System.Windows.Documents;
using Tavernkeep.Statblock.Parser.Entities;
using Tavernkeep.Statblock.Parser.Parsing.Abstractions;
using Tavernkeep.Statblock.Parser.Parsing.Tokenizers;

namespace Tavernkeep.Statblock.Parser.Parsing.Factories
{
	public static class TokenizerFactory
	{
		public static ITokenizer<T> GetTokenizer<T>() where T : class, new()
		{
			return typeof(T) switch
			{
				Type t when t == typeof(PathbuilderRecord) => (new PathbuilderTokenizer() as ITokenizer<T>)!,
				Type t when t == typeof(FlowDocument) => (new FlowDocumentTokenizer() as ITokenizer<T>)!,
				_ => throw new NotImplementedException(),
			};
		}
	}
}
