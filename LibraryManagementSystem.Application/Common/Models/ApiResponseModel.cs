namespace LibraryManagementSystem.Application.Common.Models;

public class ApiResponseModel<T>
{
    public bool IsSuccess { get; set; }
    public int StatusCode { get; set; }
    public string Message { get; set; } = string.Empty;
    public T? Data { get; set; }
    public List<string>? Errors { get; set; }

    public static ApiResponseModel<T> SuccessResponse(T data, string message, int statusCode)
    {
        return new ApiResponseModel<T>
        {
            IsSuccess = true,
            StatusCode = statusCode,
            Message = message,
            Data = data
        };
    }

    public static ApiResponseModel<T> FailureResponse(string message, int statusCode, List<string>? errors = null)
    {
        return new ApiResponseModel<T>
        {
            IsSuccess = false,
            StatusCode = statusCode,
            Message = message,
            Errors = errors
        };
    }
}