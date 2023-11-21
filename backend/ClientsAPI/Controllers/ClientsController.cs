using ClientsAPI.DTO;
using ClientsAPI.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace ClientsAPI.Controllers
{
    [Route("api/clients")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly IClientService _clientService;

        public ClientsController(IClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Client>>> List()
        {
            //AWSXRayRecorder.Instance.BeginSegment("Clients API");
            var clients = await _clientService.ListAsync();
            //AWSXRayRecorder.Instance.EndSegment();
            return clients;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Client>> GetAsync(string id)
        {
            var result = await _clientService.GetAsync(id);
            return result;
        }

        [HttpPost]
        public async Task<ActionResult<Client>> Create(Client client)
        {
            var result = await _clientService.CreateAsync(client);
            return result;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Client>> Update(string id, [FromBody] Client newClient)
        {
            var result = await _clientService.UpdateAsync(id, newClient);
            return result;
        }
    }
}
