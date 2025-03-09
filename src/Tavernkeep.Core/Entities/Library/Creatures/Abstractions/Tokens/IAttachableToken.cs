using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tavernkeep.Core.Entities.Library.Creatures.Abstractions.Tokens
{
	public interface IAttachableToken
	{
		public bool IsAttachable(IToken token);
		public void Attach(IToken token);
	}
}
