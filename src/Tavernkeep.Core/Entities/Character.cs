using Tavernkeep.Core.Entities.Base;

namespace Tavernkeep.Core.Entities
{
    public class Character : Entity
    {
        #region Constructors

        public Character(string data)
        {
            Data = data;
        }

        #endregion

        #region Properties

        public User Owner { get; set; } = default!;
        public string Data { get; set; }

        #endregion
    }
}
