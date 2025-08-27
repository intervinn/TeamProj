using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shared.Models
{
    public class Lesson
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        public required string Subject { get; set; } // Название предмета
        public required TimeSpan StartTime { get; set; } // Время начала урока
        public required TimeSpan EndTime { get; set; } // Время окончания урока

        public required int TeacherId { get; set; } // Идентификатор учителя
        public required int ScheduleId { get; set; } // Идентификатор расписания

        [ForeignKey(nameof(TeacherId))]
        public required Teacher Teacher { get; set; } // Связь с учителем

        [ForeignKey(nameof(ScheduleId))]
        public required Schedule Schedule { get; set; } // Связь с расписанием
    }
}
