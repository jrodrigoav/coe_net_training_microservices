using Microsoft.AspNetCore.Mvc;
using RentingAPI.Models;
using RentingAPI.Models.DTO;
using RentingAPI.Services;
using System.Text.Json;


namespace RentingAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RentingController(RentingService rentingService, InventoryApi inventoryApi, ClientApi clientsApi, ResourceApi resourceApi) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult> GetAll()
    {
        try
        {
            var rents = await rentingService.GetAllRents();
            return Ok(new { Items = rents, Count = rents.Count() });
        }
        catch (Exception ex)
        {
            return Ok(new { ex.Message });
        }
    }

    [HttpGet("client/{clientId}")]
    public async Task<ActionResult<IEnumerable<RentingDTO>>> GetRentsByClient(string clientId, bool returned = false)
    {
        var clientRents = await rentingService.GetRentsByClient(clientId, returned);
        var populatedRents = new List<RentingDTO>();
        foreach (var clientRent in clientRents)
        {
            var rentResource = await resourceApi.Get(clientRent.ResourceId);
            var rentingDto = JsonSerializer.Deserialize<RentingDTO>(JsonSerializer.Serialize(clientRent))!;
            rentingDto.ResourceName = rentResource.Name;
            populatedRents.Add(rentingDto);
        }
        return Ok(populatedRents);
    }

    [HttpPost("register")]
    public async Task<ActionResult> Register(Renting renting)
    {
        var resourceInventories = await inventoryApi.GetResourceInventory(renting.ResourceId);
        if (!resourceInventories.Any())
        {
            return BadRequest("The resource is not available.");
        }
        var copyId = resourceInventories.First().Id;
        renting.CopyId = copyId;

        var client = clientsApi.Get(renting.ClientId);
        if (client == null)
        {
            return BadRequest("The client doesn't exist.");
        }
        var clientRent = await rentingService.CreateRentForResource(renting);

        await inventoryApi.SetCopyAvailability(copyId, false);
        return Ok(clientRent);
    }

    [HttpPut("return/{id}")]
    public async Task<ActionResult> Return(string id, ReturnResourceDTO returnResourceDto)
    {
        var renting = await rentingService.Get(id);
        if(renting.Returned) return BadRequest($"The copy {id} is already returned.");
        var copyId = renting.CopyId ?? throw new NullReferenceException();

        var rentingReturned = await rentingService.SetAsReturned(id, returnResourceDto.ReturnDate);
        if (!rentingReturned) throw new Exception($"Failed to set renting {id} as returned");
        await inventoryApi.SetCopyAvailability(copyId, true);
        return Ok(new { Message = $"The copy with ID {copyId} has been returned." });
    }
}