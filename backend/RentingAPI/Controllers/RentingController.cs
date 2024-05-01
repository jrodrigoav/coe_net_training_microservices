using Microsoft.AspNetCore.Mvc;
using RentingAPI.Extensions;
using RentingAPI.Models;
using RentingAPI.Models.InventoryAPI;
using RentingAPI.Services;
using RentingAPI.Services.Data;

namespace RentingAPI.Controllers
{
    [Route("api/renting"), ApiController]
    public class RentingController : ControllerBase
    {

        [Route("api/resources"), ApiController]
        public class ResourcesController : ControllerBase
        {
            [HttpGet("list")]
            public ActionResult List([FromServices]RentingDbContext rentingDbContext)
            {
                return Ok();
            }

            [HttpGet("list/{clientId}")]
            public ActionResult ListByClientId([FromRoute]Guid clientId, [FromServices] RentingDbContext rentingDbContext, [FromQuery]bool returned = false)
            {
                return Ok();
            }

            [HttpPost("register")]
            public async Task<ActionResult> Register([FromBody]RegisterRentRequest registerRentRequest, [FromServices]InventoryAPIClient inventoryAPIClient, [FromServices]RentingDbContext rentingDbContext)
            {
                var items = await inventoryAPIClient.ListResourceAvailabilityAsync(registerRentRequest.ResourceId);
                if (items.Length <= 0) return Ok(new { Message = "The resource is not available." });
                var rent = new Models.Data.Rent
                {
                    CopyId = items[0].Id,
                    ResourceId = registerRentRequest.ResourceId,
                    ClientId = registerRentRequest.ClientId,
                    RegistrationDate = registerRentRequest.RegistrationDate,
                    ReturnDate = registerRentRequest.ReturnDate
                };
                rentingDbContext.Add(rent);
                await rentingDbContext.SaveChangesAsync();
                var updateItemRequest = new UpdateItemRequest
                {
                    ItemId = rent.CopyId,
                    Available = false
                };
                await inventoryAPIClient.UpdateItemAvailabilityAsync(updateItemRequest);
                return Ok(rent.ToRentResponse());
            }
        }
    }
}
