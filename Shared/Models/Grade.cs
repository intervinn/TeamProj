using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Shared.Models
{
    public class Grade
    {
        [JsonPropertyName("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        [JsonPropertyName("added_at")]
        public DateTime AddedAt { get; set; }
        [JsonPropertyName("removed_at")]
        public DateTime? RemovedAt { get; set; }

        [JsonPropertyName("value")]
        public string Value { get; set; } = string.Empty;
        [JsonPropertyName("commentary")]
        public string Commentary { get; set; } = string.Empty;

        [JsonPropertyName("teacher_id")]
        public int TeacherId { get; set; }
        [JsonPropertyName("student_id")]
        public int StudentId { get; set; }

        [JsonIgnore]
        [ForeignKey(nameof(StudentId))]
        public required Student Student { get; set; }

        [JsonIgnore]
        [ForeignKey(nameof(TeacherId))]
        public required Student Teacher { get; set; }
    }
}
