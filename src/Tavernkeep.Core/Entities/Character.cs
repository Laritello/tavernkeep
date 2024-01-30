using Tavernkeep.Core.Entities.Base;

namespace Tavernkeep.Core.Entities
{
    public class Character : Entity
    {
        #region Constructors

        public Character(Character owner, Session session, string data)
        {
            Owner = owner;
            Session = session;
            Data = data;
        }

        public Character(Character owner, Session session) : this(owner, session, string.Empty) { }

        #endregion

        #region Properties

        public Character Owner { get; set; }
        public Session Session { get; set; }
        public string Data { get; set; }

        #endregion
    }
}
