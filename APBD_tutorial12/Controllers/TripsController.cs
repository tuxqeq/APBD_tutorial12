using APBD_tutorial12.DTOs;
using APBD_tutorial12.Services;
using Microsoft.AspNetCore.Mvc;

namespace APBD_tutorial12.Controllers
{
    [ApiController]
    [Route("api/trips")]
    public class TripsController : ControllerBase
    {
        private readonly ITripService _tripService;
        private readonly IClientService _clientService;

        public TripsController(ITripService tripService, IClientService clientService)
        {
            _tripService = tripService;
            _clientService = clientService;
        }

        [HttpGet]
        public IActionResult GetTrips([FromQuery] int? page, [FromQuery] int? pageSize)
        {
            int p = page ?? 1;
            int ps = pageSize ?? 10;
            var result = _tripService.GetTrips(p, ps);
            return Ok(result);
        }

        [HttpPost("{idTrip}/clients")]
        public IActionResult RegisterClient(int idTrip, [FromBody] NewClientDto dto)
        {
            try
            {
                _clientService.RegisterClientToTrip(idTrip, dto);
                return CreatedAtAction(nameof(GetTrips), new { id = idTrip }, dto);
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { error = "Trip not found." });
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { error = ex.Message });
            }
        }
    }
}