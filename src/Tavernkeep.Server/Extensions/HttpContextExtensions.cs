using Tavernkeep.Core.Contracts.Authentication;
using Tavernkeep.Core.Exceptions;

namespace Tavernkeep.Server.Extensions
{
    /// <summary>
    /// The <see cref="HttpContextExtensions"/> class contains extension methods that enhance the capabilities of the <see cref="HttpContext"/> class.
    /// </summary>
    public static class HttpContextExtensions
    {
        /// <summary>
        /// Retrieves the user ID from the <see cref="HttpContext"/>. It is designed to provide a convenient way to access the ID of the user associated with the current HTTP request.
        /// </summary>
        /// <param name="context">Represents the current HTTP context, containing information about the current request and its associated user.</param>
        /// <returns>The user ID associated with the current HTTP request.</returns>
        /// <exception cref="NotAuthorizedException">Thrown when the user is not authorized.</exception>
        public static Guid GetUserId(this HttpContext context)
        {
            var claim = context.User.FindFirst(JwtCustomClaimNames.UserId)
                ?? throw new NotAuthorizedException("Access denied. Please provide valid credentials.");

            return Guid.Parse(claim.Value);
        }
    }
}
