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
        private readonly ResourcesAPIClient _resourcesAPIClient;
        private readonly RentingDbContext _rentingDbContext;

        public RentingController(InventoryAPIClient inventoryAPIClient,  RentingDbContext rentingDbContext,ResourcesAPIClient resourcesAPIClient) { 
            _inventoryAPIClient = inventoryAPIClient;
            _resourcesAPIClient = resourcesAPIClient;
            _rentingDbContext = rentingDbContext;
        }

        [HttpGet("{resourceId}")]
        public ActionResult List()//[FromServices] RentingDbContext rentingDbContext)
        {
            var rents = _rentingDbContext.Rents.AsNoTracking();
            return Ok(new { items = rents.Select(r => r.ToRentResponse()).ToArray(), count = rents.Count() });
        }

        [HttpGet("rented/{clientId}")]
        public async Task<ActionResult<object>> ItemsRentedByClientId(Guid clientId)
        {
            try
            {
        
                var r =  _rentingDbContext.Rents.Where(x => x.ClientId == clientId);// .FindAsync(parms);
            if (r == null) return NotFound();

                /////////////
                ///

                List<Models.Data.Rent> info = new List<Models.Data.Rent>();

                foreach(var item in r)
                {
                    var resourceInfo = await _resourcesAPIClient.GetResourceByIdAsync(item.ResourceId);// _resourcesAPIClient.GetResourceByIdAsync(item.ResourceId);
                    var rent = new Models.Data.Rent
                    {
                        Id = item.ResourceId,
                        CopyId = item.Id,
                        ResourceId = item.ResourceId,
                        ClientId = item.ClientId,
                        RegistrationDate = item.RegistrationDate.ToUniversalTime(),
                        ReturnDate = item.ReturnDate,
                        Name = resourceInfo.Name,
                        ResourceName = resourceInfo.Name,
                    };

                    info.Add(rent);
                }
              
                //_rentingDbContext.Add(rent);
                //await _rentingDbContext.SaveChangesAsync();
                //var updateItemRequest = new UpdateItemRequest
                //{
                //    ItemId = rent.CopyId,
                //    Available = false
                //};
                //await _inventoryAPIClient.UpdateItemAvailabilityAsync(updateItemRequest);
                //var data = rent.ToRentResponse();


                //////////////////

                var data = info;//.ToResourceResponse();
            //  Debug.WriteLine("Data ",data.ToString());
            return Ok(data);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPut("return/{resourceId}")]
        public async Task<ActionResult<object>> ReturnByResourceById(Guid resourceId, [FromBody] ReturnInfo returnInfo)
        {
            var resourceInfo = await _resourcesAPIClient.GetResourceByIdAsync(resourceId);
            //update it with returned and the return date
            
            return Ok(resourceInfo);
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
            var rentDaysSpan = 15;
            var items = await _inventoryAPIClient.ListResourceAvailabilityAsync(registerRentRequest.ResourceId);
            if (items.Length <= 0) return Ok(new { Message = "The resource is not available." });
            var rent = new Models.Data.Rent
            {
                CopyId = items[0].Id,
                ResourceId = registerRentRequest.ResourceId,
                ClientId = registerRentRequest.ClientId,
                RegistrationDate = registerRentRequest.RegistrationDate.ToUniversalTime(),
               // ReturnDate = registerRentRequest.RegistrationDate.AddDays(rentDaysSpan).ToUniversalTime(),
                Name = items[0].Name
            };
            _rentingDbContext.Add(rent);
            await _rentingDbContext.SaveChangesAsync();
            var updateItemRequest = new UpdateItemRequest
            {
                ItemId = rent.CopyId,
                Available = false
            };
            await _inventoryAPIClient.UpdateItemAvailabilityAsync(updateItemRequest);
            var data = rent.ToRentResponse();
            return Ok(data);

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

    public class ReturnInfo
    {
       // [JsonProperty("returnDate")]
        public string ReturnDate { get; set; }
    }
}
