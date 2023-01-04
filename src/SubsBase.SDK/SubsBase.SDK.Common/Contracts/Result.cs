namespace SubsBase.SDK.Common.Contracts;

public class Result<T>
{
    public Result(T value)
    {
        IsSuccess = true;
        Value = value;
    }
    public T? Value { get; set; }
    public bool IsSuccess { get; set; }
}