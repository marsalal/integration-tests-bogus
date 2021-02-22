using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using integration_tests.Data;
using integration_tests.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace integration_tests.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : Controller
    {
        private  SampleContext _context {get;set;}
        public PersonController(SampleContext context)
        {
            _context = context;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Person>), 200)]
        public IActionResult GetAll()
        {
            var result =_context.Persons.ToList();
            return Ok(result);
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(Person), 200)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetById(string id)
        {
            if (string.IsNullOrEmpty(id)) { return BadRequest(); }

            var result = _context.Persons.FirstOrDefault(x => x.Id == id);
            if (result == null) { return NotFound(); }
            return Ok(result);
        }

        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult AddPerson([FromBody] Person person)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            _context.Persons.Add(person);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetById), new { id = person.Id }, person);
        }

        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult DeletePerson([FromQuery]string id)
        {
            if (string.IsNullOrEmpty(id)) { return BadRequest(); }

            var personToDelete = _context.Persons.FirstOrDefault(x => x.Id == id);
            if(personToDelete == null) { return NotFound(); }
            _context.Persons.Remove(personToDelete);
            _context.SaveChanges();

            return Ok();
        }
    }
}
