using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using integration_tests.Data;
using integration_tests.Data.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace integration_tests.Controllers
{
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

        [Route("{id}")]
        [HttpGet]
        public IActionResult GetById(string id)
        {
            if (string.IsNullOrEmpty(id)) return BadRequest();

            var result = _context.Persons.FirstOrDefault(x => x.Id == id);
            return Ok(result);
        }

        [HttpPost]
        public IActionResult AddPerson([FromBody] Person person)
        {
            if (ModelState.IsValid)
            {
                return BadRequest();
            }

            _context.Persons.Add(person);
            _context.SaveChanges();

            return Ok();
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeletePerson(string id)
        {
            return Ok();
        }
    }
}
