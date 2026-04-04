using System.Net;
using System.Text.Json;
using LibraryManagementSystem.Application.Common.Models;

namespace LibraryManagementSystem.API.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;
    
    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
            // handle 401 and 403 after pipeline completes
            if (context.Response.StatusCode == 401 && !context.Response.HasStarted)
            {
                context.Response.ContentType = "application/json";
                var json = JsonSerializer.Serialize(new ErrorResponse
                {
                    StatusCode = 401,
                    Message = "You are not authenticated. Please login first"
                }, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
                await context.Response.WriteAsync(json);
            }

            if (context.Response.StatusCode == 403 && !context.Response.HasStarted)
            {
                context.Response.ContentType = "application/json";
                var json = JsonSerializer.Serialize(new ErrorResponse
                {
                    StatusCode = 403,
                    Message = "You do not have permission to access this resource"
                }, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
                await context.Response.WriteAsync(json);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            await HandleExceptionAsync(context, ex);
        }
    }
    
    private static async Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        context.Response.ContentType = "application/json";
    
        var response = ex switch
        {
            KeyNotFoundException => new ErrorResponse
            {
                StatusCode = (int)HttpStatusCode.NotFound,
                Message = ex.Message
            },
            UnauthorizedAccessException => new ErrorResponse
            {
                StatusCode = (int)HttpStatusCode.Unauthorized,
                Message = ex.Message
            },
            ArgumentException => new ErrorResponse
            {
                StatusCode = (int)HttpStatusCode.BadRequest,
                Message = ex.Message
            },
            InvalidOperationException => new ErrorResponse
            {
                StatusCode = (int)HttpStatusCode.BadRequest,
                Message = ex.Message
            },
            _ => new ErrorResponse
            {
                StatusCode = (int)HttpStatusCode.InternalServerError,
                Message = "An unexpected error occurred",
                Details = ex.Message
            }
        };
    
        context.Response.StatusCode = response.StatusCode;
    
        var json = JsonSerializer.Serialize(response, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });
    
        await context.Response.WriteAsync(json);
    }

}