using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Tavernkeep.Server.Exceptions.Handlers
{
    /// <summary>
    /// This class represents an exception handler for non-specific exceptions in the application. It implements the <see cref="IExceptionHandler"/> interface and provides a mechanism to handle business logic exceptions and log them using a logger.
    /// </summary>
    /// <param name="logger">The logger instance.</param>
    public class GenericExceptionHandler(ILogger<GenericExceptionHandler> logger) : IExceptionHandler
    {
        /// <inheritdoc/>
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            logger.LogError(exception, "Unhandled exception occurred: {Message}", exception.Message);

            var problemDetails = new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "Unhandled exception",
                Detail = exception.Message
            };

            httpContext.Response.StatusCode = problemDetails.Status.Value;

            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);
            return true;
        }
    }
}
