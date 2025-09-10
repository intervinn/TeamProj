using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Shared.Models
{
    public class Teacher
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("first_name")]
        public required string FirstName { get; set; }
        [JsonPropertyName("last_name")]
        public required string LastName { get; set; }

        [JsonPropertyName("born_at")]
        public DateTime BornAt { get; set; }
        [JsonPropertyName("hired_at")]
        public DateTime HiredAt { get; set; }
    }
}
