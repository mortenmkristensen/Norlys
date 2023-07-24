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
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OfficeLocation>>> Get(CancellationToken cancellationToken) 
        {
            var officeLocations = await _officeLocationService.GetAllOfficeLocations(cancellationToken);
            return Ok(officeLocations);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<OfficeLocation>> Get(int id, CancellationToken cancellationToken) 
        {
            var officeLocation = await _officeLocationService.GetOfficeLocationByID(id, cancellationToken);
            if (officeLocation == null) {
                return NotFound();
            }
            return Ok(officeLocation);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Post([FromBody] OfficeLocation officeLocation, CancellationToken cancellationToken) 
        {
            await _officeLocationService.CreateOfficeLocation(officeLocation, cancellationToken);
            return CreatedAtAction(nameof(Get), new { id = officeLocation.OfficeID }, officeLocation);
        }

        [HttpPut()]
        public async Task<IActionResult> Put([FromBody] OfficeLocation officeLocation, CancellationToken cancellationToken) 
        {
            var existingOfficeLocation = await _officeLocationService.GetOfficeLocationByID(officeLocation.OfficeID, cancellationToken);
            if (existingOfficeLocation == null) {
                return NotFound();
            }

            await _officeLocationService.UpdateOfficeLocation(officeLocation, cancellationToken);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken) 
        {
            var existingOfficeLocation = await _officeLocationService.GetOfficeLocationByID(id, cancellationToken);
            if (existingOfficeLocation == null) {
                return NotFound();
            }

            await _officeLocationService.DeleteOfficeLocation(id, cancellationToken);
            return NoContent();
        }
    }
}
