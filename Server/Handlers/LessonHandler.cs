
using Server.Services;

namespace Server.Handlers
{
    class LessonHandler : IMessageHandler
    {
        private StorageService _storage;

        public LessonHandler(
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
