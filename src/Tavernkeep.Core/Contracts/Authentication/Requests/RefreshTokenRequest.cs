namespace Tavernkeep.Core.Contracts.Authentication.Requests
{
    public class RefreshTokenRequest
    {
        public string AccessToken { get; set; } = default!;
        public string RefreshToken { get; set; } = default!;
    }
}
