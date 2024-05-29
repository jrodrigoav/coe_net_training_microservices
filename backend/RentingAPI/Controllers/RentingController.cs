using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        private readonly InventoryAPIClient _inventoryAPIClient;
        private readonly RentingDbContext _rentingDbContext;

        public RentingController(InventoryAPIClient inventoryAPIClient,  RentingDbContext rentingDbContext) { 
            _inventoryAPIClient = inventoryAPIClient;
            _rentingDbContext = rentingDbContext;
        }

        [HttpGet("{resourceId}")]
        public ActionResult List()//[FromServices] RentingDbContext rentingDbContext)
        {
            var rents = _rentingDbContext.Rents.AsNoTracking();
            return Ok(new { items = rents.Select(r => r.ToRentResponse()).ToArray(), count = rents.Count() });
        }


        //[Route("api/resources"), ApiController]//I think this class def should not be here
        //public class ResourcesController : ControllerBase
        //{
        //    [HttpGet("list")]
        //    public ActionResult List([FromServices] RentingDbContext rentingDbContext)
        //    {
        //        var rents = rentingDbContext.Rents.AsNoTracking();
        //        return Ok(new { items = rents.Select(r => r.ToRentResponse()).ToArray(), count = rents.Count() });
        //    }

        //    [HttpGet("list/{clientId}")]
        //    public async Task<ActionResult<RentResponse[]>> ListByClientId([FromRoute] Guid clientId, [FromServices] RentingDbContext rentingDbContext, [FromServices] ResourcesAPIClient resourcesAPIClient, [FromQuery] bool returned = false)
        //    {
        //        var rents = rentingDbContext.Rents.Where(r => r.ClientId == clientId && r.Returned == returned).AsNoTracking();
        //        var response = new List<RentResponse>();
        //        foreach (var r in rents)
        //        {
        //            var rr = r.ToRentResponse();
        //            var resourceResponse = await resourcesAPIClient.GetResourceByIdAsync(r.ResourceId);
        //            rr.ResourceName = resourceResponse?.Name;

        //        }
        //        return Ok(new { items = response.ToArray(), count = response.Count() });
        //    }

        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] RegisterRentRequest registerRentRequest)//, [FromServices] InventoryAPIClient inventoryAPIClient, [FromServices] RentingDbContext rentingDbContext)
        {
            try
            {//TODO  this will be fixed once we know what should happen when the register button is clicked
                var items = await _inventoryAPIClient.ListResourceAvailabilityAsync(registerRentRequest.ResourceId);
                if (items.Length <= 0) return Ok(new { Message = "The resource is not available." });
                var rent = new Models.Data.Rent
                {
                    CopyId = items[0].Id,
                    ResourceId = registerRentRequest.ResourceId,
                    ClientId = registerRentRequest.ClientId,
                    RegistrationDate = registerRentRequest.RegistrationDate,
                    ReturnDate = registerRentRequest.ReturnDate
                };
                _rentingDbContext.Add(rent);
                await _rentingDbContext.SaveChangesAsync();
                var updateItemRequest = new UpdateItemRequest
                {
                    ItemId = rent.CopyId,
                    Available = false
                };
                await _inventoryAPIClient.UpdateItemAvailabilityAsync(updateItemRequest);
                return Ok(rent.ToRentResponse());
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message); 
                return BadRequest(ex.Message);
            }
        }

        //    [HttpPut("return")]
        //    public async Task<ActionResult> Return([FromBody] ReturnRent returnRent, RentingDbContext rentingDbContext, [FromServices] InventoryAPIClient inventoryAPIClient)
        //    {

        //        var rent = await rentingDbContext.Rents.FindAsync(returnRent.Id);
        //        if (rent == null) return NotFound();
        //        rent.ReturnDate = returnRent.ReturnDate;
        //        rent.Returned = true;
        //        await rentingDbContext.SaveChangesAsync();

        //        // Set copy availability to true in the inventory
        //        var updateItemRequest = new UpdateItemRequest
        //        {
        //            ItemId = rent.CopyId,
        //            Available = true
        //        };
        //        await inventoryAPIClient.UpdateItemAvailabilityAsync(updateItemRequest);

        //        return Ok(new { message = $"The copy with ID {rent.CopyId} has been returned." });
        //    }
        //}
    }
}
