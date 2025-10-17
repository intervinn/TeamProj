using System.Text.Json.Serialization;

namespace Client.Models;

public class Teacher
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("first_name")]
    public string? FirstName { get; set; }

    [JsonPropertyName("last_name")]
    public string? LastName { get; set; }

    [JsonPropertyName("born_at")]
    public DateTime BornAt { get; set; }

    [JsonPropertyName("hired_at")]
    public DateTime HiredAt { get; set; }
}
