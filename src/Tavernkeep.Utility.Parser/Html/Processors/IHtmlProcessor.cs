using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tavernkeep.Utility.Parser.Html.Processors
{
	public interface IHtmlProcessor
	{
		string Process(string html);
	}
}
