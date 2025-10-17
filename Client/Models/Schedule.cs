using System.Text.Json.Serialization;

namespace Client.Models;

public class Schedule
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("date")]
    public DateTime Date { get; set; }

    [JsonPropertyName("grade_id")]
    public int GradeId { get; set; }

    [JsonPropertyName("lessons")]
    public ICollection<int> Lessons { get; set; } = new List<int>();
}
