using System.Text.Json.Serialization;

namespace SubsBase.SDK.Balance.Contracts;

public class PaginationResult<T>
{
    [JsonPropertyName("pageNumber")] public int PageNumber { get; set; }
    [JsonPropertyName("pageSize")] public int PageSize { get; set; }
    [JsonPropertyName("totalRecords")]public int TotalRecords { get; set; }
    [JsonPropertyName("data")] public List<T> Data { get; set; }
}