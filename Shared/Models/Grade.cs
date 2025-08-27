using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shared.Models
{
    public class Grade
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        public DateTime AddedAt { get; set; }
        public DateTime? RemovedAt { get; set; }

        public string Value { get; set; } = string.Empty;
        public string Commentary { get; set; } = string.Empty;

        public int TeacherId { get; set; }
        public int StudentId { get; set; }

        [ForeignKey(nameof(StudentId))]
        public required Student Student { get; set; }

        [ForeignKey(nameof(TeacherId))]
        public required Student Teacher { get; set; }
    }
}
