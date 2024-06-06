﻿using Tavernkeep.Core.Contracts.Interfaces;
using Tavernkeep.Core.Entities.Modifiers;

namespace Tavernkeep.Core.Entities
{
    public class Condition : IModifierSource
    {
        #region Backing fields

        private readonly List<Modifier> _modifiers = [];

        #endregion

        #region Constructors

        public Condition(string name)
        {
            Name = name;
        }

        #endregion

        #region Properties

        public string Name { get; set; }
        public Condition? Secondary { get; set; }
        public IReadOnlyCollection<Modifier> Modifiers => _modifiers.AsReadOnly();

        #endregion
    }
}
