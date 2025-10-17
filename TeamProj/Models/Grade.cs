using System.Text.Json.Serialization;

namespace TeamProj.Models;

public class Grade
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("added_at")]
    public DateTime AddedAt { get; set; }

    [JsonPropertyName("removed_at")]
    public DateTime? RemovedAt { get; set; }

    [JsonPropertyName("value")]
    public string? Value { get; set; }

    [JsonPropertyName("commentary")]
    public string? Commentary { get; set; }

    [JsonPropertyName("teacher_id")]
    public int TeacherId { get; set; }

    [JsonPropertyName("student_id")]
    public int StudentId { get; set; }
}
