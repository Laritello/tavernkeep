using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tavernkeep.Core.Statblocks.Abstractions.Tokens
{
	public interface IKeywordToken : IToken, IAttachableToken
	{
		public string Name { get; set; }
		public bool IsCheckResult { get; }
	}
}
