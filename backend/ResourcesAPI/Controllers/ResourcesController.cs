using Microsoft.AspNetCore.Mvc;
using ResourcesAPI.Models;
using ResourcesAPI.Services.Interface;

namespace ResourcesAPI.Controllers
{
    [Route("api/resources")]
    [ApiController]
    public class ResourcesController : ControllerBase
    {
        private readonly IResourcesService _resourcesService;
        public ResourcesController(IResourcesService resourcesService)
        {
            _resourcesService = resourcesService;
        }
        [HttpGet]
        public async Task<List<ResourceModel>> List()
        {
            var result = await _resourcesService.ListAsync();
            return result;
        }

        [HttpGet("{id}")]
        public async Task<ResourceModel> Get(string id) 
        {
            var result = await _resourcesService.GetAsync(id);
            return result;
        }

        [HttpPost] 
        public async Task<ResourceModel> Create([FromBody] ResourceModel Model) 
        {
            var result = await _resourcesService.CreateAsync(Model);            
            return result;
        }

        [HttpPut] 
        public async Task<ResourceModel> Update([FromBody] ResourceModel model) 
        {
            var resut = await _resourcesService.UpdateAsync(model);
            return resut;
        }

        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            _resourcesService.DeleteAsync(id);
        }
    }
}
