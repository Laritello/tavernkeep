namespace Tavernkeep.Core.Contracts.Authentication.Requests
{
    public class AuthenticationRequest(string login, string password)
    {
        public string Login { get; set; } = login;
        public string Password { get; set; } = password;
    }
}
