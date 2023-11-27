using Microsoft.AspNetCore.Mvc;
using RentingAPI.Contracts;
using RentingAPI.DTO;

namespace RentingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentingController : ControllerBase
    {

        private readonly IRentingService _rentingService;

        public RentingController(IRentingService rentingService)
        {
            _rentingService = rentingService;
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            try
            {
                var result = await _rentingService.GetAllAsync();
                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Server error: {e}");
            }
        }

        [HttpGet("clients/{id}")]
        public async Task<IActionResult> ListByClientID(Guid id)
        {
            try
            {
                var result = await _rentingService.GetByClientIDAsync(id);
                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Server error: {e}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] Renting renting)
        {
            try
            {
                var result = await _rentingService.RegisterAsync(renting);
                return CreatedAtAction(nameof(GetByID), new { id = result.ID }, result);
            }
            catch (Exception)
            {
                return StatusCode(500, "Server error");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByID(Guid id)
        {
            try
            {
                var result = await _rentingService.GetByIDAsync(id);
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500, "Server error");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Return(Guid id)
        {
            try
            {
                var renting = await _rentingService.GetByIDAsync(id);
                if (renting == null) return NotFound(id);

                var result = await _rentingService.ReturnAsync(id);
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500, "Server error");
            }
        }

        [HttpGet("/{id}/copies")]
        public async Task<IActionResult> GetCopyIDByRentingID(Guid id)
        {
            try
            {
                var renting = await _rentingService.GetByIDAsync(id);
                if (renting == null) return NotFound(id);

                return Ok(renting.CopyID);
            }
            catch (Exception)
            {
                return StatusCode(500, "Server error");
            }
        }

        [HttpPut("/copies/{id}")]
        public async Task<IActionResult> SetCopyAvailability(Guid id)
        {
            try
            {
                var result = await _rentingService.SetCopyAvailabilityAsync(id);
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500, "Server error");
            }
        }
    }
}
