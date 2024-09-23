
namespace Subsbase.Balance.Inputs;

public class SortingInput
{
    public string? SortBy { get; set; } 
    public SortingDirection? SortDirection { get; set; } 
}

public enum SortingDirection
{
    Ascending = 1,
    Descending
}
