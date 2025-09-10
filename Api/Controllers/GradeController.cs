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

        [HttpGet]
        public async Task<IActionResult> GetGrades()
        {
            var result = await _storage.Context.Grades.ToListAsync();
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateGrade([FromBody] Grade grade)
        {
            try
            {
                var message = new Message
                {
                    Action = "Edit",
                    ModelType = "Grade",
                    Data = grade
                };
                await _producer.SendAsync(message);
                return Ok("Запрос отправлен");
            } catch (Exception e)
            {
                return StatusCode(500, "Запрос не удалось отправить");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateGrade([FromBody] Grade grade)
        {
            try
            {
                var message = new Message
                {
                    Action = "Create",
                    ModelType = "Grade",
                    Data = grade
                };
                await _producer.SendAsync(message);
                return Ok("Запрос отправлен");
            }
            catch (Exception e)
            {
                return StatusCode(500, "Запрос не удалось отправить");
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteGrade([FromBody] Grade grade)
        {
            try
            {
                var message = new Message
                {
                    Action = "Delete",
                    ModelType = "Grade",
                    Data = grade
                };
                await _producer.SendAsync(message);
                return Ok("Запрос отправлен");
            }
            catch (Exception e)
            {
                return StatusCode(500, "Запрос не удалось отправить");
            }
        }
    }
}
