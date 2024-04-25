using Microsoft.AspNetCore.Mvc;
using ResourcesAPI.Models;
using ResourcesAPI.Services;


namespace ResourcesAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ResourceController(ResourceService resourcesService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Resource>>> GetAll()
    {
        var resources = await resourcesService.GetAll();
        return Ok(resources);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Resource>> GetById(string id)
    {
        var resource = await resourcesService.GetById(id);
        if (resource == null)
        {
            return NotFound();
        }
        return Ok(resource);
    }

    [HttpPost]
    public async Task<ActionResult> Create(Resource resource)
    {
        var createdResource = await resourcesService.Create(resource);
        return CreatedAtAction(nameof(GetById), new { id = createdResource.Id }, createdResource);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] string id, [FromBody] Resource resource)
    {
        if (id != resource.Id)
        {
            return BadRequest();
        }
        var updatedResource = await resourcesService.Update(id, resource);
        return Ok(updatedResource);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var result = await resourcesService.Delete(id);
        if(!result) return BadRequest($"No resource found by id {id}");
        return NoContent();
    }
}