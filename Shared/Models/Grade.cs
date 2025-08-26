using System.ComponentModel.DataAnnotations.Schema;

namespace Shared.Models
{
    public class Grade
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public DateTime AddedAt { get; set; }
        public DateTime? RemovedAt { get; set; }

        public string Value { get; set; } = string.Empty;
        public string Commentary { get; set; } = string.Empty;

        public int TeacherId { get; set; }
        public int StudentId { get; set; }
    }
}
