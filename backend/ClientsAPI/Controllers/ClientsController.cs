using ClientsAPI.Contracts;
using ClientsAPI.DTO;
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

        [HttpPost]
        public async Task<ActionResult<Client>> Create(Client client)
        {
            //AWSXRayRecorder.Instance.BeginSegment("Clients API");
            var createdClient = await _clientService.CreateAsync(client);
            //AWSXRayRecorder.Instance.EndSegment();
            return CreatedAtAction(nameof(GetById), new { id = createdClient.Id }, createdClient);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Client>> Update(string id, Client newClientData)
        {
            // AWSXRayRecorder.Instance.BeginSegment("Clients API");
            var client = await _clientService.GetAsync(id);
            // AWSXRayRecorder.Instance.EndSegment();

            newClientData.Id = id;

            if (client == null) return NotFound();

            //AWSXRayRecorder.Instance.BeginSegment("Clients API");
            await _clientService.UpdateAsync(newClientData);
            //AWSXRayRecorder.Instance.EndSegment();

            return Ok(newClientData);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Client>> Delete(string id)
        {
            // AWSXRayRecorder.Instance.BeginSegment("Clients API");
            var client = await _clientService.GetAsync(id);
            // AWSXRayRecorder.Instance.EndSegment();

            if (client == null) return NotFound();

            //AWSXRayRecorder.Instance.BeginSegment("Clients API");
            await _clientService.DeleteAsync(id);
            //AWSXRayRecorder.Instance.EndSegment();

            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Client>> GetById(string id)
        {
            // AWSXRayRecorder.Instance.BeginSegment("Clients API");
            var client = await _clientService.GetAsync(id);
            // AWSXRayRecorder.Instance.EndSegment();

            if (client == null) return NotFound();

            //AWSXRayRecorder.Instance.BeginSegment("Clients API");
            //AWSXRayRecorder.Instance.EndSegment();

            return Ok(client);
        }
    }
}
