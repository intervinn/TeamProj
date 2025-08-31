
using Server.Services;

namespace Server.Handlers
{
    class ScheduleHandler : IMessageHandler
    {
        private StorageService _storage;

        public ScheduleHandler(
            StorageService storage
        )
        {
            _storage = storage;
        }

        public async Task CreateAsync(object data)
        {

        }
        public async Task EditAsync(object data)
        {

        }
        public async Task DeleteAsync(object data)
        {

        }
    }
}
