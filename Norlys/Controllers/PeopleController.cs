using Microsoft.AspNetCore.Mvc;
using Norlys.Domain;
using Norlys.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Norlys.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private readonly IPeopleService _peopleService;
        private readonly PersonValidator _personValidator;

        public PeopleController(IPeopleService peopleService, PersonValidator personValidator) 
        {
            _peopleService = peopleService;
            _personValidator = personValidator;
        }

        // GET: api/<PeopleController>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<Person>>> Get() {
            var people = await _peopleService.GetAllPeople();
            if (!people.Any()) 
            {
                return NotFound();
            }
            return Ok(people);
        }

        // GET api/<PeopleController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Person>> Get(int id) 
        {
            var person = await _peopleService.GetPersonByID(id);
            if (person == null) {
                return NotFound();
            }
            return Ok(person);
        }

        // POST api/<PeopleController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] Person person) 
        {
            await _peopleService.CreatePerson(person);
            if (!await _personValidator.Validate(person)) {
                return BadRequest("Person invalid. Remove whitespaces from LastName, make sure birthdate is valid or make sure MaxOccupancy for OfficeLocation is not reached");
            }
            return CreatedAtAction(nameof(Get), new { id = person.PersonID }, person);
        }

        // PUT api/<PeopleController>/5
        [HttpPut()]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Put([FromBody] Person person) 
        {
            var existingPerson = await _peopleService.GetPersonByID(person.PersonID);
            if (existingPerson == null) {
                return NotFound();
            }
            if (!await _personValidator.Validate(person)) 
            {
                return BadRequest("Person invalid. Remove whitespaces from LastName, make sure birthdate is valid or make sure MaxOccupancy for OfficeLocation is not reached");
            }

            await _peopleService.UpdatePerson(person);
            return NoContent();
        }

        // DELETE api/<PeopleController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id) 
        {
            var existingPerson = await _peopleService.GetPersonByID(id);
            if (existingPerson == null) {
                return NotFound();
            }

            await _peopleService.DeletePerson(id);
            return NoContent();
        }
    }
}

