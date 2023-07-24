using Microsoft.AspNetCore.Mvc;
using Norlys.Domain;
using Norlys.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Norlys.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OfficeLocationsController : ControllerBase
    {
        private readonly IOfficeLocationService _officeLocationService;

        public OfficeLocationsController(IOfficeLocationService officeLocationService) 
        {
            _officeLocationService = officeLocationService;
        }
        // GET: api/<OfficeLocationsController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OfficeLocation>>> Get() 
        {
            var officeLocations = await _officeLocationService.GetAllOfficeLocations();
            return Ok(officeLocations);
        }

        // GET api/<OfficeLocationsController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<OfficeLocation>> Get(int id) 
        {
            var officeLocation = await _officeLocationService.GetOfficeLocationByID(id);
            if (officeLocation == null) {
                return NotFound();
            }
            return Ok(officeLocation);
        }

        // POST api/<OfficeLocationsController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Post([FromBody] OfficeLocation officeLocation) 
        {
            await _officeLocationService.CreateOfficeLocation(officeLocation);
            return CreatedAtAction(nameof(Get), new { id = officeLocation.OfficeID }, officeLocation);
        }

        // PUT api/<OfficeLocationsController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] OfficeLocation officeLocation) 
        {
            var existingOfficeLocation = await _officeLocationService.GetOfficeLocationByID(id);
            if (existingOfficeLocation == null) {
                return NotFound();
            }

            await _officeLocationService.UpdateOfficeLocation(officeLocation);
            return NoContent();
        }

        // DELETE api/<OfficeLocationsController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id) 
        {
            var existingOfficeLocation = await _officeLocationService.GetOfficeLocationByID(id);
            if (existingOfficeLocation == null) {
                return NotFound();
            }

            await _officeLocationService.DeleteOfficeLocation(id);
            return NoContent();
        }
    }
}
