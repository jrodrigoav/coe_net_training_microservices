using Microsoft.AspNetCore.Mvc;
using InventoryAPI.Models;
using InventoryAPI.Services;
using Core.Services;
using InventoryAPI.Models.DTO;

namespace InventoryAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class InventoryController(InventoryService inventoryService, ResourceApi resourceApi) : ControllerBase
{
    [HttpGet("{id}")]
    public async Task<ActionResult<Inventory>> GetById(string id)
    {
        var inventory = await inventoryService.Get(id);
        if (inventory == null)
        {
            return NotFound();
        }
        return Ok(inventory);
    }

    [HttpGet("resource/{resourceId}")]
    public async Task<ActionResult<IEnumerable<Inventory>>> GetResourceInventory(string resourceId, [FromQuery] bool available = true)
    {
        var inventories = await inventoryService.GetByResourceId(resourceId, available);
        return Ok(inventories);
    }

    [HttpGet("summary")]
    public async Task<ActionResult<IEnumerable<InventorySummary>>> GetSummary()
    {
        var resources = await resourceApi.GetAll();
        if (resources == null)
            return StatusCode(StatusCodes.Status500InternalServerError);

        var populatedResources = resources.Select(async resource =>
        {
            var available = await inventoryService.GetCountByResourceId(resource.Id, true);
            var unavailable = await inventoryService.GetCountByResourceId(resource.Id, false);
            return new InventorySummary
            {
                ResourceId = resource.Id,
                ResourceName = resource.Name,
                AvailableCopies = available,
                UnavailableCopies = unavailable,
                TotalCopies = available + unavailable
            };
        }).Select(t => t.Result);

        return Ok(populatedResources);
    }

    [HttpPost("register")]
    public async Task<ActionResult<Inventory>> Register(Inventory inventory)
    {
        var resource = await resourceApi.Get(inventory.ResourceId);
        if (resource == null)
        {
            return BadRequest($"Couldn't find resource by id {inventory.ResourceId}");
        }
        var createdInventory = await inventoryService.Create(inventory);
        return CreatedAtAction(nameof(GetById), new { id = createdInventory.Id }, createdInventory);
    }

    [HttpPut("setAvailability/{id}")]
    public async Task<ActionResult> SetAvailability(string id, UpdateAvailabilityDTO updateAvailabilityDto)
    {
        var inventory = await inventoryService.Get(id);
        if (inventory == null)
        {
            return NotFound($"Couldn't find inventory by id {id}");
        }
        inventory.Available = updateAvailabilityDto.Available;
        await inventoryService.Update(id, inventory);
        return Ok(new { Message = "Updated successfully." });
    }
}