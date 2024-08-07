﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentingAPI.Extensions;
using RentingAPI.Models;
using RentingAPI.Models.InventoryAPI;
using RentingAPI.Models.ResourcesAPI;
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
        public ActionResult List()
        {
            var rents = _rentingDbContext.Rents.AsNoTracking();
            return Ok(new { items = rents.Select(r => r.ToRentResponse()).ToArray(), count = rents.Count() });
        }

        [HttpGet("rented/{clientId}")]
        public async Task<ActionResult<object>> ItemsRentedByClientId(Guid clientId)
        {
            var r = _rentingDbContext.Rents.Where(x => x.ClientId == clientId && x.Returned == false);
            if (r == null) return NotFound();
            List<Models.Data.Rent> info = new List<Models.Data.Rent>();
            foreach (var item in r)
            {
                var resourceInfo = await _resourcesAPIClient.GetResourceByIdAsync(item.ResourceId);
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
            var data = info;
            return Ok(data);
        }

        [HttpPut("return/{resourceId}")]
        public async Task<ActionResult<object>> ReturnByResourceById(Guid resourceId, [FromBody] ReturnInfo returnInfo)
        {
            var resourceInfo = await _resourcesAPIClient.GetResourceByIdAsync(resourceId);
            var item = _rentingDbContext.Rents.FirstOrDefault(r => r.ResourceId == resourceId);
            var a = await ReturnItem(item, returnInfo);
            return Ok(resourceInfo);
        }

        private async Task<int> ReturnItem(Models.Data.Rent? rent, ReturnInfo returnInfo)
        {
            rent.ReturnDate = DateTime.Parse(returnInfo.ReturnDate);
            rent.Returned = true;
            _rentingDbContext.Rents.Update(rent);
            return await _rentingDbContext.SaveChangesAsync();
        }


        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] RegisterRentRequest registerRentRequest)
        {
            var items = await _inventoryAPIClient.ListResourceAvailabilityAsync(registerRentRequest.ResourceId);
            if (items.Length <= 0) return Ok(new { Message = "The resource is not available." });
            var rent = new Models.Data.Rent
            {
                CopyId = items[0].Id,
                ResourceId = registerRentRequest.ResourceId,
                ClientId = registerRentRequest.ClientId,
                RegistrationDate = registerRentRequest.RegistrationDate.ToUniversalTime(),
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

        
    }

    public class ReturnInfo
    {
        public string ReturnDate { get; set; }
    }
}
