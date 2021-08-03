using eintech.api.Services;
using eintech.domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public PersonController(IPersonDeleteService deleteService,IPersonReadService readService,IPersonUpdateService updateService)
        {
            _deleteService = deleteService;
            _readService = readService;
            _updateService = updateService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var people = await _readService.Get();

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

            return Ok(await _updateService.Create(person));
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest();

            await _deleteService.Delete(id);

            return Ok();
        }

    }
}
