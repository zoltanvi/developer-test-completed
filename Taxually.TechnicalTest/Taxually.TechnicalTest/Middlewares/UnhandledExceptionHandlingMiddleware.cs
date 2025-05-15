using Microsoft.AspNetCore.Mvc;

namespace Taxually.TechnicalTest.Middlewares;

public class UnhandledExceptionHandlingMiddleware
{
    private const string UserFriendlyErrorTitle = "Something went wrong";
    private const string UserFriendlyErrorMessage = "Something went wrong. Please try again later.";
    private readonly RequestDelegate _next;

    public UnhandledExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            // We should log the exception here!
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await context.Response.WriteAsJsonAsync(new ProblemDetails
            {
                Title = UserFriendlyErrorTitle,
                Detail = UserFriendlyErrorMessage
            });
        }
    }
}