using InventoryAPI.Contracts;
using InventoryAPI.DTO;
using Microsoft.AspNetCore.Mvc;

namespace InventoryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryService _inventoryService;

        public InventoryController(IInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }

        [HttpPost]
        public async Task<IActionResult> Register(Inventory inventory)
        {
            try
            {
                var result = await _inventoryService.RegisterInventoryAsync(inventory);
                return CreatedAtAction(nameof(GetInventoryByID), new { id = result.ID }, result);
            }
            catch (Exception)
            {
                return StatusCode(500, "Server error");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetInventoryByID(Guid id)
        {
            try
            {
                var inventory = await _inventoryService.GetInventoryByIDAsync(id);
                if (inventory == null) return NotFound(id);

                return Ok(inventory);
            }
            catch (Exception)
            {
                return StatusCode(500, "Server error");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Summary()
        {
            try
            {
                var inventory = await _inventoryService.GetAllAsync();
                return Ok(inventory);
            }
            catch (Exception)
            {
                return StatusCode(500, "Server error");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Inventory inventory, Guid id)
        {
            try
            {
                var existingInventory = await _inventoryService.GetInventoryByIDAsync(id);
                if (existingInventory is null) return NotFound(id);

                var updatedInventory = _inventoryService.UpdateAsync(id, inventory);

                return Ok(updatedInventory);
            }
            catch (Exception)
            {
                return StatusCode(500, "Server error");
            }
        }

    }
}
