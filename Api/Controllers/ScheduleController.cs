using Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shared.Models;

namespace Api.Controllers
{
    [ApiController]
    [Route("schedules")]
    public class ScheduleController : ControllerBase
    {
        private StorageService _storage;
        private MessageProduceService _producer;

        public ScheduleController(StorageService storage, MessageProduceService producer)
        {
            _storage = storage;
            _producer = producer;
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetSchedule(int id)
            => Ok(await _storage.Context.Schedules.Select(p => p.Id == id).FirstAsync());
        [HttpGet]
        public async Task<IActionResult> GetSchedules()
            => Ok(await _storage.Context.Schedules.ToListAsync());
        [HttpPut]
        public async Task<IActionResult> UpdateSchedule([FromBody] Schedule Schedule)
            => await _producer.HandleSendAsync("Edit", "Schedule", Schedule);
        [HttpPost]
        public async Task<IActionResult> CreateSchedule([FromBody] Schedule Schedule)
            => await _producer.HandleSendAsync("Create", "Schedule", Schedule);
        [HttpDelete]
        public async Task<IActionResult> DeleteSchedule([FromBody] Schedule Schedule)
            => await _producer.HandleSendAsync("Delete", "Schedule", Schedule);
    }
}
