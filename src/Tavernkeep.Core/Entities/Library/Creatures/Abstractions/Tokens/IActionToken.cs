using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tavernkeep.Core.Contracts.Enums;

namespace Tavernkeep.Core.Entities.Library.Creatures.Abstractions.Tokens
{
	public interface IActionToken : IToken
	{
		public ActionAmount Amount { get; set; }
	}
}
