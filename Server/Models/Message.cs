
namespace Server.Models
{
    public class Message
    {
        public required string Action { get; set; }
        public required string ModelType { get; set; }
        public object Data { get; set; }
    }
}
