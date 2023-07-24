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

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<Person>>> Get(CancellationToken cancellationToken) {
            var people = await _peopleService.GetAllPeople(cancellationToken);
            if (!people.Any()) 
            {
                return NotFound();
            }
            return Ok(people);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Person>> Get(int id, CancellationToken cancellationToken) 
        {
            var person = await _peopleService.GetPersonByID(id, cancellationToken);
            if (person == null) {
                return NotFound();
            }
            return Ok(person);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] Person person, CancellationToken cancellationToken) 
        {
            if (!await _personValidator.Validate(person, cancellationToken)) {
                return BadRequest("Person invalid. Remove whitespaces from LastName, make sure birthdate is valid or make sure MaxOccupancy for OfficeLocation is not reached");
            }
            await _peopleService.CreatePerson(person, cancellationToken);
            return CreatedAtAction(nameof(Get), new { id = person.PersonID }, person);
        }

        [HttpPut()]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Put([FromBody] Person person, CancellationToken cancellationToken) 
        {
            var existingPerson = await _peopleService.GetPersonByID(person.PersonID, cancellationToken);
            if (existingPerson == null) {
                return NotFound();
            }
            if (!await _personValidator.Validate(person, cancellationToken)) 
            {
                return BadRequest("Person invalid. Remove whitespaces from LastName, make sure birthdate is valid or make sure MaxOccupancy for OfficeLocation is not reached");
            }

            await _peopleService.UpdatePerson(person, cancellationToken);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken) 
        {
            var existingPerson = await _peopleService.GetPersonByID(id, cancellationToken);
            if (existingPerson == null) {
                return NotFound();
            }

            await _peopleService.DeletePerson(id, cancellationToken);
            return NoContent();
        }
    }
}

