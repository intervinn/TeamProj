
namespace Server.Handlers
{
    public interface IMessageHandler
    {
        public Task CreateAsync(object data);
        public Task EditAsync(object data);
        public Task DeleteAsync(object data);
    }
}
