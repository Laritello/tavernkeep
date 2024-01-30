using Tavernkeep.Core.Entities.Base;

namespace Tavernkeep.Core.Entities
{
    public class User : Entity
    {
        #region Backing fields

        private readonly List<Character> _characters = [];

        #endregion

        #region Constructors

        public User(Session session, string login, string password) 
        {
            Session = session;
            Login = login;
            Password = password;
        }

        #endregion

        #region Properties

        public string Login { get; set; }
        public string Password { get; set; }
        public Session Session { get; set; }
        public IReadOnlyList<Character> Characters => _characters.AsReadOnly();

        #endregion
    }
}
