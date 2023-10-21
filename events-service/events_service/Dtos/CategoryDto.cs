using System.Text.Json.Serialization;

namespace events_service.Dtos;

public class CategoryDto
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    [JsonPropertyName("name")]
    public string Name { get; set; } = null!;
}
