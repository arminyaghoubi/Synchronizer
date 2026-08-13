namespace Synchronizer.Domain.Common;

public class Result
{
    public bool IsSuccess { get; private set; }
    public string? Error { get; private set; }

    private Result() { }

    protected Result(bool success, string? error)
    {
        if (success && !string.IsNullOrEmpty(error))
            throw new InvalidOperationException("Success result can not be have error");

        if (!success && string.IsNullOrEmpty(error))
            throw new InvalidOperationException("Failed result must be have error");

        IsSuccess = success;
        Error = error;
    }

    public static Result Success() => new(true, null);
    public static Result Failure(string error) => new(false, error);
}

public class Result<TData> : Result
{
    public TData? Data { get; private set; }

    protected Result(bool isSuccess, string? error, TData? data)
        : base(isSuccess, error)
    {
        Data = data;
    }

    public static Result<TData> Success(TData data) => new(true, null, data);
    public static new Result<TData> Failure(string error) => new(false, error, default(TData));
}