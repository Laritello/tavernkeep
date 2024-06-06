using Tavernkeep.Core.Entities.Base;
using Tavernkeep.Core.Entities.Modifiers;

namespace Tavernkeep.Core.Entities
{
    public class ConditionInfo : Entity
    {
        #region Backing fields

        private readonly List<Modifier> _modifiers = [];

        #endregion

        #region Constructors

        public ConditionInfo(string name, string description)
        {
            Name = name;
            Description = description;
        }

        #endregion

        #region Properties

        public string Name { get; set; }
        public string Description { get; set; }
        public ConditionInfo? Secondary { get; set; }
        public IReadOnlyCollection<Modifier> Modifiers => _modifiers.AsReadOnly();

        #endregion
    }
}
