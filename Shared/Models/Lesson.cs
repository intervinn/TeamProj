using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Shared.Models
{
    public class Lesson
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("subject")]
        public required string Subject { get; set; } // Название предмета
        [JsonPropertyName("start_time")]
        public required DateTime StartTime { get; set; } // Время начала урока
        [JsonPropertyName("end_time")]
        public required DateTime EndTime { get; set; } // Время окончания урока

        [JsonPropertyName("teacher_id")]
        public required int TeacherId { get; set; } // Идентификатор учителя
        [JsonPropertyName("schedule_id")]
        public required int ScheduleId { get; set; } // Идентификатор расписания

        [ForeignKey(nameof(TeacherId))]
        [JsonIgnore]
        public required Teacher Teacher { get; set; } // Связь с учителем

        [ForeignKey(nameof(ScheduleId))]
        [JsonIgnore]
        public required Schedule Schedule { get; set; } // Связь с расписанием
    }
}
