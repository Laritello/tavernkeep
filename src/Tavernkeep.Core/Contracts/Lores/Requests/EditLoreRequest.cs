using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tavernkeep.Core.Contracts.Enums;

namespace Tavernkeep.Core.Contracts.Lores.Requests
{
    public class EditLoreRequest
    {
        public Guid CharacterId { get; set; }
        public string Topic { get; set; } = default!;
        public Proficiency Proficiency { get; set; } = default!;
    }
}
