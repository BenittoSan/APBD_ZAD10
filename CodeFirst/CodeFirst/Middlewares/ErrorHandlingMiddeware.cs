using System.Net;
using CodeFirst.Exceptions;

namespace CodeFirst.Middlewares;

public class ErrorHandlingMiddeware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ErrorHandlingMiddeware> _logger;

    public ErrorHandlingMiddeware(RequestDelegate next, ILogger<ErrorHandlingMiddeware> logger)
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
        catch (DomainException dx)
        {
            _logger.LogError(dx,"Bad request");
            await HandleDomainExceptionAsync(context, dx);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unhandled eception occurred");

            await HandleExceptionAsync(context, ex);
        }
    }

    public Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        context.Response.ContentType = "application/json";

        var response = new
        {
            error = new
            {
                message = "An error occurred while processing your requset.",
                detail = exception.Message
            }
        };
        var jsonResponse = System.Text.Json.JsonSerializer.Serialize(response);

        return context.Response.WriteAsync(jsonResponse);
    }
    public Task HandleDomainExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
        context.Response.ContentType = "application/json";

        var response = new
        {
            error = new
            {
                message = "An error occurred while processing your requset.",
                detail = exception.Message
            }
        };
        var jsonResponse = System.Text.Json.JsonSerializer.Serialize(response);

        return context.Response.WriteAsync(jsonResponse);
    }
    

}