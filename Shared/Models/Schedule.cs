using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Shared.Models
{
    public class Schedule
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("date")]
        public required DateTime Date { get; set; } // Дата расписания
        [JsonPropertyName("grade_id")]
        public required int GradeId { get; set; } // Идентификатор класса

        [ForeignKey(nameof(GradeId))]
        [JsonIgnore]
        public required Grade Grade { get; set; } // Связь с классом

        [JsonPropertyName("lessons")]
        public ICollection<int> Lessons { get; set; } = []; // Уроки в расписании
    }
}
