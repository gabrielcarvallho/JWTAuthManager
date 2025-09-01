namespace JWTAuthManager.Application.Common.Models;

public class Result
{
    public bool IsSuccess { get; protected set; }
    public string Message { get; protected set; } = string.Empty;
    public List<string> Errors { get; protected set; } = new();

    protected Result(bool isSuccess, string message, List<string>? errors = null)
    {
        IsSuccess = isSuccess;
        Message = message;
        Errors = errors ?? new List<string>();
    }

    public static Result Success(string message = "Operation completed successfully")
        => new(true, message);

    public static Result Failure(string message, List<string>? errors = null)
        => new(false, message, errors);

    public static Result Failure(string message, params string[] errors)
        => new(false, message, errors.ToList());
}

public class Result<T> : Result
{
    public T? Data { get; private set; }

    private Result(bool isSuccess, string message, T? data = default, List<string>? errors = null)
        : base(isSuccess, message, errors)
    {
        Data = data;
    }

    public static Result<T> Success(T data, string message = "Operation completed successfully")
        => new(true, message, data);

    public static new Result<T> Failure(string message, List<string>? errors = null)
        => new(false, message, default, errors);

    public static new Result<T> Failure(string message, params string[] errors)
        => new(false, message, default, errors.ToList());
}