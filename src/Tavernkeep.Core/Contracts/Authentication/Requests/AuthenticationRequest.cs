namespace Tavernkeep.Core.Contracts.Authentication.Requests
{
    /// <summary>
    /// Represents an authentication request.
    /// </summary>
    public class AuthenticationRequest
    {
        /// <summary>
        /// The user's login.
        /// </summary>
        public string Login { get; set; } = default!;

        /// <summary>
        /// The user's password.
        /// </summary>
        public string Password { get; set; } = default!;
    }
}
