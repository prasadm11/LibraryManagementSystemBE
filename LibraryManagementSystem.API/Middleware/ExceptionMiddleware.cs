using System.Net;
using System.Text.Json;
using LibraryManagementSystem.Application.Common.Models;

namespace LibraryManagementSystem.API.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(
        RequestDelegate next,
        ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);

            // Handle 401
            if (context.Response.StatusCode == 401 &&
                !context.Response.HasStarted)
            {
                context.Response.ContentType = "application/json";

                var response = ApiResponseModel<object>
                    .FailureResponse(
                        "You are not authenticated. Please login first",
                        401
                    );

                var json = JsonSerializer.Serialize(
                    response,
                    new JsonSerializerOptions
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    });

                await context.Response.WriteAsync(json);
            }

            // Handle 403
            if (context.Response.StatusCode == 403 &&
                !context.Response.HasStarted)
            {
                context.Response.ContentType = "application/json";

                var response = ApiResponseModel<object>
                    .FailureResponse(
                        "You do not have permission to access this resource",
                        403
                    );

                var json = JsonSerializer.Serialize(
                    response,
                    new JsonSerializerOptions
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    });

                await context.Response.WriteAsync(json);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);

            await HandleExceptionAsync(context, ex);
        }
    }

    private static async Task HandleExceptionAsync(
        HttpContext context,
        Exception ex)
    {
        context.Response.ContentType = "application/json";

        var response = ex switch
        {
            KeyNotFoundException =>
                ApiResponseModel<object>.FailureResponse(
                    ex.Message,
                    (int)HttpStatusCode.NotFound
                ),

            UnauthorizedAccessException =>
                ApiResponseModel<object>.FailureResponse(
                    ex.Message,
                    (int)HttpStatusCode.Unauthorized
                ),

            ArgumentException =>
                ApiResponseModel<object>.FailureResponse(
                    ex.Message,
                    (int)HttpStatusCode.BadRequest
                ),

            InvalidOperationException =>
                ApiResponseModel<object>.FailureResponse(
                    ex.Message,
                    (int)HttpStatusCode.BadRequest
                ),

            _ =>
                ApiResponseModel<object>.FailureResponse(
                    "An unexpected error occurred",
                    (int)HttpStatusCode.InternalServerError,
                    new List<string> { ex.Message }
                )
        };

        context.Response.StatusCode = response.StatusCode;

        var json = JsonSerializer.Serialize(
            response,
            new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

        await context.Response.WriteAsync(json);
    }
}