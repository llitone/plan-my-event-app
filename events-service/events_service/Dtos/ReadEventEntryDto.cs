using System.Text.Json.Serialization;

namespace events_service.Dtos;

public class ReadEventEntryDto
{
    [JsonPropertyName("user_id")]
    public int UserId { get; set; }
    [JsonPropertyName("event_id")]
    public int EventId { get; set; }
    [JsonPropertyName("signed_at")]
    public DateTime SignedAt { get; set; }
}
