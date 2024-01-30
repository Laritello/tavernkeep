using Tavernkeep.Core.Entities.Base;

namespace Tavernkeep.Core.Entities
{
    public class Session : Entity
    {
        #region Backing fields

        private readonly List<Character> users = [];

        #endregion

        #region Constructors

        public Session(string name)
        {
            Name = name;
        }

        #endregion

        #region Properties

        public string Name { get; set; }

        public IReadOnlyList<Character> Users
        {
            get => users.AsReadOnly();
        }

        #endregion
    }
}
