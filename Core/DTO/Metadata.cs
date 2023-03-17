using Newtonsoft.Json;

namespace UnitessTestApp.Api.Core.DTO;

public class Metadata
{
    [JsonProperty("nextCursor")]
    public string? NextCursor { get; set; }

    [JsonProperty("previousCursor")]
    public string? PreviousCursor { get; set; }

    [JsonProperty("totalPages")]
    public int TotalPages { get; set; }

    [JsonProperty("totalSize")]
    public int? TotalSize { get; set; }

}