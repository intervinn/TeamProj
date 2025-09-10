
namespace Shared.Models
{
    public class Message
    {
        public required string Action { get; set; }
        public required string ModelType { get; set; }
        public required object Data { get; set; }
    }
}
