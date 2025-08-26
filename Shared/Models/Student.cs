
using System.ComponentModel.DataAnnotations.Schema;

namespace Shared.Models
{
    public class Student
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public required string FirstName { get; set; }
        public required string LastName { get; set; }

        public DateTime BornAt { get; set; }
        public DateTime EnlistedAt { get; set; }
    }
}
