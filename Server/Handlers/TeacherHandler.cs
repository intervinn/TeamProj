
using Microsoft.EntityFrameworkCore;
using Server.Services;
using Shared.Models;

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
            if (data is Teacher g)
            {
                await _storage.Context.Teachers.AddAsync(g);
                await _storage.Context.SaveChangesAsync();
            }
        }
        public async Task EditAsync(object data)
        {
            if (data is Teacher g)
            {
                var obj = await _storage.Context.Teachers.SingleAsync(p => p.Id == g.Id);
                if (obj == null)
                {
                    throw new Exception("Обьект не найден");
                }

                var entry = _storage.Context.Entry(obj);
                entry.CurrentValues.SetValues(g);

                foreach (var property in entry.Properties)
                {
                    var original = entry.OriginalValues[property.Metadata.Name];
                    var current = entry.CurrentValues[property.Metadata.Name];
                    if (Equals(original, current))
                    {
                        property.IsModified = false;
                    }
                }

                await _storage.Context.SaveChangesAsync();
            }
        }
        public async Task DeleteAsync(object data)
        {
            if (data is Teacher g)
            {
                _storage.Context.Teachers.Remove(g);
                await _storage.Context.SaveChangesAsync();
            }
        }
    }
}
