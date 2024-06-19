using InventoryAPI.Extensions;
using InventoryAPI.Models;
using InventoryAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace InventoryAPI.Controllers
{

    [Route("api/inventory"), ApiController, Produces("application/json")]
    //public class InventoryController(InventoryService inventoryService) : ControllerBase
    public class InventoryController : ControllerBase
    {
        private  InventoryService _inventoryService1;
        public  InventoryController(InventoryService inventoryService)
        { 
            _inventoryService1=inventoryService;
        }


        [HttpPost("register")]
        public async Task<ActionResult<ItemResponse>> Register([FromBody] RegisterItemRequest registerItemRequest)
        {
            var item = await _inventoryService1.RegisterAsync(registerItemRequest.ToItem());
            if (item == null) return NotFound();
            return Ok(item);
        }

        [HttpGet("list/{resourceId}")]
        public ActionResult<ItemResponse[]> ListResourceAvailability(Guid resourceId, [FromQuery] bool available = true)
        {
            var items = _inventoryService1.ListResourceAvailability(resourceId, available);
            return Ok(items.Select(i => i.ToItemResponse()).ToArray());
        }

        [HttpPut("setAvailability")]
        public async Task<ActionResult<ItemResponse[]>> UpdateItemAvailability([FromBody] UpdateItemRequest updateItemRequest)
        {
            var item = await _inventoryService1.UpdateItemAvailabilityAsync(updateItemRequest.ItemId, updateItemRequest.Available);
            if (item == null) return NotFound();
            return Ok(new { Message = "Updated successully" });
        }

        [HttpGet("summary")]
        public async Task<ActionResult<Summary[]>> GetSummary()
        {
            return Ok(await _inventoryService1.GetSummaryAsync());
        }
    }
}