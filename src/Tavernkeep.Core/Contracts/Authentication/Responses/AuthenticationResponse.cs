namespace Tavernkeep.Core.Contracts.Authentication.Responses
{
    public class AuthenticationResponse
    {
        public string? Token { get; set; }
        public string? RefreshToken { get; set; }
    }
}
