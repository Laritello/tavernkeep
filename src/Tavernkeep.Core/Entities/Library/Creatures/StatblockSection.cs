using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tavernkeep.Core.Entities.Library.Creatures
{
	public record StatblockSection(string Header, ICollection<StatblockSpan> Spans);
}
