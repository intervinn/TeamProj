using Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shared.Models;

namespace Api.Controllers
{
    [ApiController]
    [Route("grades")]
    public class GradeController : ControllerBase
    {
        private StorageService _storage;
        private MessageProduceService _producer;

        public GradeController(StorageService storage, MessageProduceService producer)
        {
            _storage = storage;
            _producer = producer;
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetGrade(int id)
            => Ok(await _storage.Context.Grades.Select(p => p.Id == id).FirstAsync());
        [HttpGet]
        public async Task<IActionResult> GetGrades()
            => Ok(await _storage.Context.Grades.ToListAsync());
        [HttpPut]
        public async Task<IActionResult> UpdateGrade([FromBody] Grade grade)
            => await _producer.HandleSendAsync("Edit", "Grade", grade);
        [HttpPost]
        public async Task<IActionResult> CreateGrade([FromBody] Grade grade)
            => await _producer.HandleSendAsync("Create", "Grade", grade);
        [HttpDelete]
        public async Task<IActionResult> DeleteGrade([FromBody] Grade grade)
            => await _producer.HandleSendAsync("Delete", "Grade", grade);
    }
}
