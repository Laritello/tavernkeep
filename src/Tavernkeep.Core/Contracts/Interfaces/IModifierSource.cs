using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tavernkeep.Core.Entities.Modifiers;

namespace Tavernkeep.Core.Contracts.Interfaces
{
    public interface IModifierSource
    {
        public string Name { get; }
        public IReadOnlyCollection<Modifier> Modifiers { get; }
    }
}
