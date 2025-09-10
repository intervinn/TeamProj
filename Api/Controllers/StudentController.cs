using Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shared.Models;

namespace Api.Controllers
{
    [ApiController]
    [Route("students")]
    public class StudentController : ControllerBase
    {
        private StorageService _storage;
        private MessageProduceService _producer;

        public StudentController(StorageService storage, MessageProduceService producer)
        {
            _storage = storage;
            _producer = producer;
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetStudent(int id)
            => Ok(await _storage.Context.Students.Select(p => p.Id == id).FirstAsync());
        [HttpGet]
        public async Task<IActionResult> GetStudents()
            => Ok(await _storage.Context.Students.ToListAsync());
        [HttpPut]
        public async Task<IActionResult> UpdateStudent([FromBody] Student Student)
            => await _producer.HandleSendAsync("Edit", "Student", Student);
        [HttpPost]
        public async Task<IActionResult> CreateStudent([FromBody] Student Student)
            => await _producer.HandleSendAsync("Create", "Student", Student);
        [HttpDelete]
        public async Task<IActionResult> DeleteStudent([FromBody] Student Student)
            => await _producer.HandleSendAsync("Delete", "Student", Student);
    }
}
