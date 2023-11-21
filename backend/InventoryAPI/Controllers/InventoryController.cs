using InventoryAPI.Models;
using InventoryAPI.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace InventoryAPI.Controllers
{
    [Route("api/inventory")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryService _inventoryService;
        public InventoryController(IInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }
        [HttpGet("list")]
        public async Task<ActionResult<List<InventoryModel>>> List([FromBody] RequestModel request)
        {
            var result = await _inventoryService.ListAsync(request);
            return result;
        }

        [HttpPost]
        public async Task<ActionResult<InventoryModel>> Register([FromBody] InventoryModel model)
        {
            var result = await _inventoryService.RegisterAsync(model);
            return result;
        }

        [HttpPut]
        public async Task<ActionResult<InventoryModel>> SetAvailability([FromBody] RequestModel request)
        {
            var result = await _inventoryService.SetAvailabilityAsync(request);
            return result;
        }

        [HttpGet]
        public async Task<ActionResult<ResponseModel>> Summary()
        {
            var result = await _inventoryService.SummaryAsync();
            return result;
        }
    }
}
