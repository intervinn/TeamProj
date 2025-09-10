using Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shared.Models;

namespace Api.Controllers
{
    [ApiController]
    [Route("lessons")]
    public class LessonController : ControllerBase
    {
        private StorageService _storage;
        private MessageProduceService _producer;

        public LessonController(StorageService storage, MessageProduceService producer)
        {
            _storage = storage;
            _producer = producer;
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetLesson(int id)
            => Ok(await _storage.Context.Lessons.Select(p => p.Id == id).FirstAsync());
        [HttpGet]
        public async Task<IActionResult> GetLessons()
            => Ok(await _storage.Context.Lessons.ToListAsync());
        [HttpPut]
        public async Task<IActionResult> UpdateLesson([FromBody] Lesson Lesson)
            => await _producer.HandleSendAsync("Edit", "Lesson", Lesson);
        [HttpPost]
        public async Task<IActionResult> CreateLesson([FromBody] Lesson Lesson)
            => await _producer.HandleSendAsync("Create", "Lesson", Lesson);
        [HttpDelete]
        public async Task<IActionResult> DeleteLesson([FromBody] Lesson Lesson)
            => await _producer.HandleSendAsync("Delete", "Lesson", Lesson);
    }
}
