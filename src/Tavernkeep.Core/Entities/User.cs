using Tavernkeep.Core.Entities.Base;

namespace Tavernkeep.Core.Entities
{
    public class User : Entity
    {
        #region Backing fields

        private readonly List<Character> _characters = [];

        #endregion

        #region Constructors

        public User(string login, string password) 
        {
            Login = login;
            Password = password;
        }       

        #endregion

        #region Properties

        public string Login { get; set; }
        public string Password { get; set; }
        public IReadOnlyList<Character> Characters => _characters.AsReadOnly();

        #endregion
    }
}
