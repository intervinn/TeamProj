using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shared.Models
{
    public class Schedule
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        public required DateTime Date { get; set; } // Дата расписания
        public required int GradeId { get; set; } // Идентификатор класса

        [ForeignKey(nameof(GradeId))]
        public required Grade Grade { get; set; } // Связь с классом

        public ICollection<Lesson> Lessons { get; set; } = new List<Lesson>(); // Уроки в расписании
    }
}
