using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tavernkeep.Core.Entities.Library.Creatures.Abstractions.Tokens
{
	public interface IKeywordToken : IToken, IAttachableToken
	{
		public string Name { get; set; }
	}
}
