using System.Text.Json.Serialization;

namespace Agriculture.Domain.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum SortField
    {
        CreatedAt,
        Name,
        Price
    }
}
