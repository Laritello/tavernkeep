namespace Tavernkeep.Core.Contracts.Authentication
{
    public class AuthenticationRequest(string login, string password)
    {
        public string Login { get; set; } = login;
        public string Password { get; set; } = password;
    }
}
