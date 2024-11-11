namespace DriveSalez.Utilities.Utilities;

public class Result<T>
{
    public T? Value { get; }
    
    public Error? Error { get; }
    
    public bool IsSuccess => Error == null;  

    private Result(T value)
    {
        Value = value;
        Error = null;
    }

    private Result(Error error)
    {
        Error = error;
        Value = default;
    }

    public static Result<T> Success(T value) => new Result<T>(value);

    public static Result<T> Failure(Error error) => new Result<T>(error);

    public TResult Map<TResult>(Func<T, TResult> success, Func<Error, TResult> failure)
    {
        return IsSuccess ? success(Value!) : failure(Error!);
    }
}