namespace Marketplace.Api.Middleware;

/// <summary>Middleware for handling exceptions and errors in the application.</summary>
public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ErrorHandlingMiddleware> _logger;

    /// <summary>Initializes a new instance of the <see cref="ErrorHandlingMiddleware"/> class.</summary>
    /// <param name="next">The next middleware in the pipeline.</param>
    /// <param name="logger">The logger instance.</param>
    public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    /// <summary>Invokes the middleware to process the HTTP request.</summary>
    /// <param name="context">The HTTP context.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (ArgumentException ex)
        {
            _logger.LogWarning("ArgumentException: {Message}", ex.Message);
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            await context.Response.WriteAsJsonAsync(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception: {Message}", ex.Message);
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await context.Response.WriteAsJsonAsync(new { message = ex.Message });
        }
    }
}
