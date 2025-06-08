using APBD_tutorial12.Services;
using Microsoft.AspNetCore.Mvc;

namespace APBD_tutorial12.Controllers
{
    [ApiController]
    [Route("api/clients")]
    public class ClientsController : ControllerBase
    {
        private readonly IClientService _clientService;

        public ClientsController(IClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpDelete("{idClient}")]
        public IActionResult DeleteClient(int idClient)
        {
            try
            {
                bool deleted = _clientService.DeleteClient(idClient);
                if (!deleted) return NotFound(new { error = "Client not found." });
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { error = ex.Message });
            }
        }
    }
}