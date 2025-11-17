using System.Text.Json;

namespace OrderProcessor.Middleware;

public class GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unhandled exception occurred");
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = "application/json";

            var apiError = new ApiError(
                StatusCodes.Status500InternalServerError.ToString(),
                ex.Message
            );

            var json = JsonSerializer.Serialize(apiError);
            await context.Response.WriteAsync(json);
        }
    }
}