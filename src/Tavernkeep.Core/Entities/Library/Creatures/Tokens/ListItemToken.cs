using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tavernkeep.Core.Entities.Library.Creatures.Abstractions.Tokens;

namespace Tavernkeep.Core.Entities.Library.Creatures.Tokens
{
	public class ListItemToken : IListItemToken
	{
		public bool IsStart { get; set; }

		public string Html => IsStart ? "<li>" : "</li>";
	}
}
