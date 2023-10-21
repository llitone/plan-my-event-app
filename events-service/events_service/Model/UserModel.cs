using System.Text.Json.Serialization;

namespace events_service.Model;

public class UserModel
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
}
