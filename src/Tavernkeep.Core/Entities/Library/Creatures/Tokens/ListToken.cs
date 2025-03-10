using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tavernkeep.Core.Entities.Library.Creatures.Tokens
{
	public class ListToken : IListToken
	{
		public bool IsStart { get; set; }

		public string Html => IsStart ? "<ul>" : "</ul>";
	}
}
