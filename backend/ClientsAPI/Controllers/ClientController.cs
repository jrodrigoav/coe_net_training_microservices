using ClientsAPI.Models;
using ClientsAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace ClientsAPI.Controllers;

[ApiController, Route("api/clients")]
public class ClientController(ClientService clientService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<Client[]>> GetAll()
    {
        var clients = await clientService.GetAll();
        return Ok(clients);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Client>> GetById(string id)
    {
        var client = await clientService.Get(id);

        if (client == null)
            return NotFound();

        return Ok(client);
    }

    [HttpPost]
    public async Task<ActionResult<Client>> Create(Client client)
    {
        var createdClient = await clientService.Create(client);
        return CreatedAtAction(nameof(GetById), new { id = createdClient.Id }, createdClient);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Client>> Update(string id, Client updatedClient)
    {
        if (id != updatedClient.Id)
        {
            return BadRequest();
        }
        var client = clientService.Get(id);

        if (client == null)
            return NotFound($"Couldn't find client by id {id}");

        await clientService.Update(id, updatedClient);
        return Ok(updatedClient);
    }
}
