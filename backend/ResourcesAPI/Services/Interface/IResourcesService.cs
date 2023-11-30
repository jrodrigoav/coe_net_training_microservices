using ResourcesAPI.Models;

namespace ResourcesAPI.Services.Interface
{
    public interface IResourcesService
    {
        public Task<List<ResourceModel>> ListAsync();
        public Task<ResourceModel> GetAsync(string id);
        public Task<ResourceModel> CreateAsync(ResourceModel Model);
        public Task<ResourceModel> UpdateAsync(ResourceModel model);
        public void DeleteAsync(string id);

    }
}
