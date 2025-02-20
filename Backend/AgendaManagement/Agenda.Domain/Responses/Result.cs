using System.Net;

namespace Agenda.Domain.Responses;

public class Result<T>
{
    public T Data { get; }
    public bool IsSuccess { get; }
    public HttpStatusCode StatusCode { get; }
    public string Message { get; }
    public List<string> Errors { get; }

    private Result(T data, bool isSuccess, HttpStatusCode statusCode, string message, List<string> errors)
    {
        Data = data;
        IsSuccess = isSuccess;
        StatusCode = statusCode;
        Message = message ?? string.Empty;
        Errors = errors ?? new List<string>();
    }

    public static Result<T> Success(T data, HttpStatusCode statusCode = HttpStatusCode.OK, string message = "")
    {
        return new Result<T>(data, true, statusCode, message, new List<string>());
    }

    public static Result<T> Failure(List<string> errors, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
    {
        return new Result<T>(default!, false, statusCode, string.Empty, errors.Count > 0 ? errors : new List<string>());
    }

    public static Result<T> Failure(string error, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
    {
        return new Result<T>(default!, false, statusCode, error, new List<string> { error });
    }
}