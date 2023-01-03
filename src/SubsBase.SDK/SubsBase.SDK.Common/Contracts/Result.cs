namespace SubsBase.SDK.Common.Contracts;

public class Result<T>
{
    public Result(T value)
    {
        value = value;
    }
    public T? Value { get; }
    public bool IsSuccess { get; set; }
}