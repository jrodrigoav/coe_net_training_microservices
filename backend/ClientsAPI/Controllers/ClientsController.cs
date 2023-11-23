using ClientsAPI.DTO;
using ClientsAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace ClientsAPI.Controllers
{
    [Route("api/clients")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly ClientService _clientService;

        public ClientsController(ClientService clientService)
        {
            _clientService= clientService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Client>>> List()
        {
            //AWSXRayRecorder.Instance.BeginSegment("Clients API");
            var clients = await _clientService.ListAsync();
            //AWSXRayRecorder.Instance.EndSegment();
            return clients;
        }
    }
}
