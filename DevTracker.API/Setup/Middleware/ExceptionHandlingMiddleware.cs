using DevTracker.Core.Application.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace DevTracker.API.Setup.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        _logger.LogError(ex, "An exception occurred while processing the request.");
        var (statusCode, title) = ex switch
        {
            NotFoundException => (StatusCodes.Status404NotFound, "Resource not found"),
            _ => (StatusCodes.Status500InternalServerError, "An internal server error occurred while processing your request.")
        };
        context.Response.StatusCode = statusCode;
        context.Response.ContentType = "application/json";
        ProblemDetails problemDetails = new ProblemDetails
        {
            Title = title,
            Status = statusCode,
            Detail = ex.Message,
            Instance = context.Request.Path
        };
        await context.Response.WriteAsJsonAsync(problemDetails);
    }
}
