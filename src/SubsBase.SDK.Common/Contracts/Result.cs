namespace SubsBase.SDK.Common.Contracts;

public class Result<T>
{
    public Result(T value)
    {
        IsSuccess = value != null;
        Value = value;
    }

    public T? Value { get; }

    public bool IsSuccess { get; }
}