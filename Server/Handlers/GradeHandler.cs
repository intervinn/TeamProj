
using Server.Services;

namespace Server.Handlers
{
    class GradeHandler : IMessageHandler
    {
        private StorageService _storage;

        public GradeHandler(
            StorageService storage
        )
        {
            _storage = storage;
        }

        public bool CanHandle(string model)
            => model == "Grade";

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
