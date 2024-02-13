using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tavernkeep.Core.Contracts.Enums;

namespace Tavernkeep.Core.Contracts.Character
{
    public class Ability
    {
        public AbilityType Type { get; set; }
        public int Score { get; set; }
    }
}
