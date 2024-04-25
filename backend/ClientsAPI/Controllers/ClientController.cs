using ClientsAPI.Extensions;
using ClientsAPI.Models;
using ClientsAPI.Models.Data;
using ClientsAPI.Services.Data;
using Microsoft.AspNetCore.Mvc;

namespace ClientsAPI.Controllers
{

    [ApiController, Route("api/clients"), Produces("application/json")]
    public class ClientsController(ClientsService clientsService) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<Client[]>> GetAll()
        {
            var clients = await clientsService.GetAllAsync();
            return Ok(clients.Select(c => c.ToClientResponse()).ToList());
        }

        [HttpGet("{clientId}")]
        public async Task<ActionResult<Client>> GetById(string clientId)
        {
            var client = await clientsService.GetAsync(clientId);

            if (client == null)
                return NotFound();

            return Ok(client.ToClientResponse());
        }

        [HttpPost]
        public async Task<ActionResult<Client>> Create(CreateClientRequest createClientRequest)
        {
            var exists = await clientsService.UserEmailExistsAsync(createClientRequest.Email);
            if (exists) return BadRequest(new { Message = "Email already registered." });
            var createdClient = await clientsService.CreateAsync(createClientRequest);
            return CreatedAtAction(nameof(GetById), new { clientId = createdClient.Id }, createdClient.ToClientResponse());
        }

        /// <response code="204">Deleted Sucessfully</response>
        [HttpDelete("{clientId}")]
        public async Task<ActionResult> Delete(string clientId)
        {

            await clientsService.DeleteAsync(clientId);
            return NoContent();
        }

        
        /// <response code="200" cref="ClientResponse">Updated Client</response>
        [HttpPut]
        public async Task<ActionResult<Client>> Update(UpdateClientRequest updateClientRequest)
        {
            var updated = await clientsService.UpdateAsync(updateClientRequest);
            return Ok(updated.ToClientResponse());
        }
    }
}