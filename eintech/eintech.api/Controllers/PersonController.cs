using Azure.Storage.Queues;
using eintech.api.Services;
using eintech.domain.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace eintech.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        IPersonDeleteService _deleteService;
        IPersonReadService _readService;
        IPersonUpdateService _updateService;
        private QueueClient _queueClient;

        public PersonController(IPersonDeleteService deleteService,IPersonReadService readService,
            IPersonUpdateService updateService,
            QueueClient queueClient)
        {
            _deleteService = deleteService;
            _readService = readService;
            _updateService = updateService;
            _queueClient = queueClient;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var people = _readService.Get().ToList();

            if (people == null)
                return NotFound();

            return Ok(people);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var person = await _readService.GetById(id);

            if (person == null)
                return NotFound();

            return Ok(person);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Person person)
        {
            if (person == null)
                return BadRequest();

            await _updateService.Create(person);
            await _queueClient.SendMessageAsync(JsonSerializer.Serialize(person),null,TimeSpan.FromSeconds(100));
            
            return Ok(person);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest();

            await _deleteService.Delete(id);

            return Ok(id);
        }

    }
}
