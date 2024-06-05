using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tavernkeep.Core.Contracts.Enums;

namespace Tavernkeep.Core.Entities.Modifiers
{
    public class Modifier
    {
        public ModifierTarget Target { get; set; }
        public int Value { get; set; }
        public bool IsBonus => Value > 0;
        public bool IsPenalty => Value < 0;
    }
}
