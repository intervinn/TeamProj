using Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shared.Models;

namespace Api.Controllers
{
    [ApiController]
    [Route("teachers")]
    public class TeacherController : ControllerBase
    {
        private StorageService _storage;
        private MessageProduceService _producer;

        public TeacherController(StorageService storage, MessageProduceService producer)
        {
            _storage = storage;
            _producer = producer;
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetTeacher(int id)
            => Ok(await _storage.Context.Teachers.Select(p => p.Id == id).FirstAsync());
        [HttpGet]
        public async Task<IActionResult> GetTeachers()
            => Ok(await _storage.Context.Teachers.ToListAsync());
        [HttpPut]
        public async Task<IActionResult> UpdateTeacher([FromBody] Teacher Teacher)
            => await _producer.HandleSendAsync("Edit", "Teacher", Teacher);
        [HttpPost]
        public async Task<IActionResult> CreateTeacher([FromBody] Teacher Teacher)
            => await _producer.HandleSendAsync("Create", "Teacher", Teacher);
        [HttpDelete]
        public async Task<IActionResult> DeleteTeacher([FromBody] Teacher Teacher)
            => await _producer.HandleSendAsync("Delete", "Teacher", Teacher);
    }
}
