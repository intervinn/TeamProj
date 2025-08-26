
using System.Text.Json.Nodes;

namespace Server.Handlers
{
    public interface IMessageHandler
    {
        public bool CanHandle(string model);

        public Task CreateAsync(object data);
        public Task EditAsync(object data);
        public Task DeleteAsync(object data);
    }
}
