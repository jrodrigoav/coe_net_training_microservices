using ClientsAPI.DTO;
using ClientsAPI.Services;
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

        //[Route("/get/{id:length(38)}")]
        [HttpGet("/get/{id:length(38)}", Name = "GetClient")]
        //[HttpGet(Name = "GetClient")]
        public async Task<ActionResult<Client>> Get(string id)
        {
            //AWSXRayRecorder.Instance.BeginSegment("Clients API");
            var client = await _clientService.GetAsync(id);
            //AWSXRayRecorder.Instance.EndSegment();

            if (client == null)
                return NotFound();

            return client;
        }

        [HttpPost]
        public async Task<ActionResult<Client>> Create(Client client)
        {
            //AWSXRayRecorder.Instance.BeginSegment("Clients API");
            await _clientService.CreateAsync(client);
            //AWSXRayRecorder.Instance.EndSegment();
            return Ok(client);
        }

        //[Route("/update/{id:length(37)}")]
        [HttpPut("/update/{id:length(37)}")]
        //[HttpPut]
        public async Task<ActionResult<Client>> Update(string id, Client newClientData)
        {
            // AWSXRayRecorder.Instance.BeginSegment("Clients API");
            var client = _clientService.GetAsync(id);
            // AWSXRayRecorder.Instance.EndSegment();

            newClientData.Id = id;

            if (client == null)
                return NotFound();

            //AWSXRayRecorder.Instance.BeginSegment("Clients API");
            await _clientService.UpdateAsync(newClientData);
            //AWSXRayRecorder.Instance.EndSegment();

            return Ok(newClientData);
        }
    }
}
