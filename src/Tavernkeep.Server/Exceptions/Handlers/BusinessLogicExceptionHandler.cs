using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Tavernkeep.Core.Exceptions;

namespace Tavernkeep.Server.Exceptions.Handlers
{
    /// <summary>
    /// This class represents an exception handler for business logic exceptions in the application. It implements the <see cref="IExceptionHandler"/> interface and provides a mechanism to handle business logic exceptions and log them using a logger.
    /// </summary>
    /// <param name="logger">The logger instance.</param>
    public class BusinessLogicExceptionHandler(ILogger<BusinessLogicExceptionHandler> logger) : IExceptionHandler
    {
        /// <inheritdoc/>
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            if (exception is not BusinessLogicException businessLogicException)
                return false;

            logger.LogError(businessLogicException, "Business logic exception occurred: {Message}", businessLogicException.Message);

            var problemDetails = new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Bad Request",
                Detail = businessLogicException.Message
            };

            httpContext.Response.StatusCode = problemDetails.Status.Value;

            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);
            return true;
        }
    }
}
