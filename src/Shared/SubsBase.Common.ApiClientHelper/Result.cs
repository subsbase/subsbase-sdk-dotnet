namespace SubsBase.Common.ApiClientHelper;

public class Result
{
    public bool IsFailed { get; set; }
    public bool IsSuccess { get; set; }
    public string? Message { get; set; }
    
    public static Result Fail(string message)
    {
        return new Result
        {
            IsFailed = true,
            Message = message
        };
    }
    
    public static Result Ok()
    {
        return new Result
        {
            IsSuccess = true
        };
    }

    public static Result<T> Ok<T>(T result) where T : class
    {
        return new Result<T>
        {
            IsSuccess = true,
            Value = result
        };
    }

    public static Result<T> Fail<T>(string message) where T : class
    {
        return new Result<T>
        {
            IsFailed = true,
            Message = message
        };
    }
}


public class Result<T>
{
    public bool IsFailed { get; set; }
    public bool IsSuccess { get; set; }
    public string? Message { get; set; }
    public T? Value { get; set; }

}