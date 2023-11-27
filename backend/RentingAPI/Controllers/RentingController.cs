using Microsoft.AspNetCore.Mvc;
using RentingAPI.Models;
using RentingAPI.Services.Interface;

namespace RentingAPI.Controllers
{
    [Route("api/renting")]
    [ApiController]
    public class RentingController : ControllerBase
    {
        private readonly IRentingService _rentingService;

        public RentingController(IRentingService rentingService)
        {
            _rentingService = rentingService;
        }
        [HttpGet]
        public async Task<ActionResult<ResponseModel>> List()
        {
            var result = await _rentingService.List();
            return result;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseModel>> ListById(string id)
        {
            var result = await _rentingService.ListById(id);
            return result;
        }

        [HttpPost]
        public async Task<ActionResult<RentingModel>> Register([FromBody] RentingModel model)
        {
            var result = await _rentingService.Register(model);
            return result;
        }

        [HttpPut("{id}")]
        public async Task<string> Return(string id)
        {
            var result = await _rentingService.Return(id);
            return result;
        }
    }
}
