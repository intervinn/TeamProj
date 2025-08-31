
using Server.Services;

namespace Server.Handlers
{
    class TeacherHandler : IMessageHandler
    {
        private StorageService _storage;

        public TeacherHandler(
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
