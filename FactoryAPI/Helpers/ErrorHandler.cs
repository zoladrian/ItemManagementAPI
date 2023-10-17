using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging; // Required for logging
using System;

namespace FactoryAPI.Helpers
{
    /// <summary>
    /// ErrorHandler class for centralized exception handling in controllers.
    /// </summary>
    public class ErrorHandler
    {
        private readonly ILogger<ErrorHandler> _logger; // Logger instance

        /// <summary>
        /// Initializes a new instance of the ErrorHandler class.
        /// </summary>
        /// <param name="logger">The logger instance.</param>
        public ErrorHandler(ILogger<ErrorHandler> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Handles various types of exceptions and returns appropriate HTTP responses.
        /// </summary>
        /// <param name="ex">The exception to handle.</param>
        /// <returns>An IActionResult that can be returned by a controller method.</returns>
        public IActionResult HandleException(Exception ex)
        {
            // Log the exception for debugging or monitoring.
            _logger.LogError(ex, "An exception occurred.");

            // Handle specific types of exceptions to return more informative responses.

            // Handle database update exceptions.
            if (ex is DbUpdateException)
            {
                return new BadRequestObjectResult(new { message = "Database update failed." });
            }

            // Handle not found exceptions.
            if (ex is KeyNotFoundException)
            {
                return new NotFoundObjectResult(new { message = "Resource not found." });
            }

            // Handle validation exceptions.
            if (ex is ArgumentException)
            {
                return new BadRequestObjectResult(new { message = "Bad arguments provided." });
            }

            // Handle unauthorized exceptions.
            if (ex is UnauthorizedAccessException)
            {
                return new UnauthorizedObjectResult(new { message = "You are not authorized to perform this action." });
            }

            // Handle forbidden exceptions.
            if (ex is InvalidOperationException && ex.Message.Contains("Forbidden"))
            {
                return new ForbidResult();
            }

            // General error handler for unknown exceptions.
            // Returns a 500 Internal Server Error status code.
            return new StatusCodeResult(500);
        }

    }
}
