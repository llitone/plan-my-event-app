using events_service_data.Entities;
using System.Text.Json.Serialization;

namespace events_service.Dtos;

public class ReadEventDto
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    [JsonPropertyName("creator_id")]
    public int CreatorId { get; set; }
    [JsonPropertyName("name")]
    public string Name { get; set; } = null!;
    [JsonPropertyName("description")]
    public string? Description { get; set; }
    [JsonPropertyName("created_at")]
    public DateTime CreatedAt { get; set; }
    [JsonPropertyName("start_at")]
    public DateTime StartAt { get; set; }
    [JsonPropertyName("address")]
    public string? Address { get; set; }
    [JsonPropertyName("price")]
    public decimal? Price { get; set; }
    
    public CategoryDto Category { get; set; } = null!;
}
