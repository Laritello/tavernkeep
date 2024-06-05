using Tavernkeep.Core.Contracts.Interfaces;
using Tavernkeep.Core.Entities.Base;
using Tavernkeep.Core.Entities.Modifiers;

namespace Tavernkeep.Core.Entities
{
    public class Condition : Entity, IModifierSource
    {
        #region Backing fields

        private readonly List<Modifier> _modifiers = [];

        #endregion

        #region Constructors

        public Condition(string name, List<Modifier> modifiers)
        {
            Name = name;
            _modifiers = modifiers;
        }

        #endregion

        #region Properties

        public string Name { get; set; }
        public Condition? Secondary { get; set; }
        public IReadOnlyCollection<Modifier> Modifiers => _modifiers.AsReadOnly();

        #endregion
    }
}
