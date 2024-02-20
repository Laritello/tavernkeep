using Microsoft.AspNetCore.Diagnostics;
using Tavernkeep.Core.Exceptions;

namespace Tavernkeep.Server.Exceptions.Handlers
{
    /// <summary>
    /// This class represents an exception handler for authorization exceptions in the application. It implements the <see cref="IExceptionHandler"/> interface and provides a mechanism to handle business logic exceptions and log them using a logger.
    /// </summary>
    /// <param name="logger">The logger instance.</param>
    public class AuthorizationExceptionHandler(ILogger<AuthorizationExceptionHandler> logger) : IExceptionHandler
    {
        /// <inheritdoc/>
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            string exceptionMessage;

            switch (exception)
            {
                case NotAuthorizedException _:
                    exceptionMessage = exception.Message;
                    logger.LogError(exception, "Authorization exception occurred: {exceptionMessage}", exception.Message);
                    httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    break;

                case InsufficientPermissionException _:
                    exceptionMessage = exception.Message;
                    logger.LogError(exception, "Insufficient permission exception occurred: {exceptionMessage}", exception.Message);
                    httpContext.Response.StatusCode = StatusCodes.Status403Forbidden;
                    break;

                default:
                    return false;
            }

            await httpContext.Response.WriteAsync(exceptionMessage, cancellationToken);
            return true;
        }
    }
}
