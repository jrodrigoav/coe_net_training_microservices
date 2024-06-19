using ClientsAPI.Extensions;
using ClientsAPI.Models;
using ClientsAPI.Models.Data;
using ClientsAPI.Services.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClientsAPI.Controllers
{

    [ApiController, Route("api/clients"), Produces("application/json")]
    public class ClientsController(ClientsDbContext clientsDbContext) : ControllerBase
    {
        [HttpGet, ProducesResponseType<ClientResponse[]>(200)]
        public async Task<ActionResult<Client[]>> GetAll()
        {
            var clients = await clientsDbContext.Clients.AsNoTracking().ToListAsync();
            return Ok(clients.Select(c => c.ToClientResponse()).ToList());
        }

        [HttpGet("{clientId}"),ProducesResponseType<ClientResponse>(200)]
        public async Task<ActionResult<Client>> GetById(Guid clientId)
        {
            var client = await clientsDbContext.Clients.FindAsync(clientId);

            if (client == null)
                return NotFound();

            return Ok(client.ToClientResponse());
        }

        [HttpPost, ProducesResponseType<ClientResponse>(201)]
        public async Task<ActionResult<Client>> Create(CreateClientRequest createClientRequest)
        {
            var client = clientsDbContext.FindByEmail(createClientRequest.Email);
            if (client != null) return BadRequest(new { Message = "Email already registered." });
            var createdClient = clientsDbContext.Add(createClientRequest.ToClient());
            await clientsDbContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { clientId = createdClient.Entity.Id }, createdClient.Entity.ToClientResponse());
        }


        [HttpDelete("{clientId}"), ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> Delete(Guid clientId)
        {
            var client = await clientsDbContext.Clients.FindAsync(clientId);
            if (client == null) return NotFound();
            clientsDbContext.Remove(client);
            await clientsDbContext.SaveChangesAsync();
            return NoContent();
        }


        [HttpPut, ProducesResponseType<ClientResponse>(200, "application/json")]
        public async Task<ActionResult<Client>> Update(UpdateClientRequest updateClientRequest)
        {
            var client = await clientsDbContext.Clients.FindAsync(updateClientRequest.Id);
            if (client == null) return NotFound();
            client.FirstName = updateClientRequest.FirstName;
            client.LastName = updateClientRequest.LastName;
            client.Email = updateClientRequest.Email;
            await clientsDbContext.SaveChangesAsync();
            return Ok(client.ToClientResponse());
        }
    }
}